// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnnotationToRulesHelper.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Microsoft Corporation
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OldSkoolGamesAndSoftware.AnnotationToRules
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OldSkoolGamesAndSoftware.Utilities;
    using OldSkoolGamesAndSoftware.Rules.Sql;
    using OldSkoolGamesAndSoftware.Rules;
    using OldSkoolGamesAndSoftware.Rules.Operators;

    /// <summary>
    /// Helper class for converting Annotation Rules to the new
    /// Rules API.
    /// </summary>
    public static class AnnotationToRulesHelper
    {
        #region Fields

        /// <summary>
        /// A repository for temporary Expression Ids.  It is possible during the translation from
        /// Annotation Rules to the new Rules API that new expressions will be created that have 
        /// no corresponding ID in the Annotation DB.  For these cases, a temporary ID will be granted.
        /// </summary>
        private static long idsForOrExpressions = -1;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the annotations from search.
        /// </summary>
        /// <param name="dal">The dal.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// An asynchronous task, the result of which is a collection of <see cref="Rule" />s.
        /// </returns>
        public static async Task<List<Rule>> GetAnnotationsFromSearch(AnnotationSqlDal dal, AnnotationSearchParameters parameters)
        {
            using (System.Data.DataSet dataSet = await dal.SelectAnnotations(parameters))
            {
                var fileTypes = new ModelTypeTable(await dal.LookupFileType(null, false));

                var rules = await LoadRules(dataSet.Tables[0]);
                await LoadRuleExpressions(rules, dataSet.Tables[1], fileTypes);
                
                return new List<Rule>(rules.Values);
            }
        }

        /// <summary>
        /// Loads the rules.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>
        /// An asynchronous task, the result of which is a dictionary of <see cref="Rule" />s, hashed on the LegacyId property.
        /// </returns>
        private static async Task<Dictionary<long, Rule>> LoadRules(System.Data.DataTable table)
        {
            return await Task.Run(() =>
            {
                var rules = new Dictionary<long, Rule>();

                foreach (System.Data.DataRow row in table.Rows)
                {
                    var rule = new Rule();

                    var solutionSourceId = Tools.Convert(row["SolutionSourceId"], default(Guid));

                    rule.SolutionSourceId = !solutionSourceId.Equals(default(Guid)) ? solutionSourceId : Guid.Empty;

                    ByteAccessibleInt64 legacyId = Tools.Convert(row["Identity"], 0L);

                    rule.Identity = new Guid(
                        0, 
                        0, 
                        0, 
                        legacyId.Byte0, 
                        legacyId.Byte1, 
                        legacyId.Byte2, 
                        legacyId.Byte3, 
                        legacyId.Byte4, 
                        legacyId.Byte5,
                        legacyId.Byte6,
                        legacyId.Byte7);

                    rule.LegacyId = legacyId;
                    rule.Author = row["UserAlias"].ToString();
                    rule.DateCreated = Tools.Convert(row["DateCreated"], default(DateTimeOffset));

                    rules.Add(rule.LegacyId, rule);
                }

                return rules;
            });
        }

        /// <summary>
        /// Loads the rule expressions.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="table">The table.</param>
        /// <param name="fileTypes">The file types.</param>
        /// <returns>
        /// An asynchronous task.
        /// </returns>
        private static async Task LoadRuleExpressions(IDictionary<long, Rule> rules, System.Data.DataTable table, ModelTypeTable fileTypes)
        {
            await Task.Run(() =>
            {
                if (table == null)
                {
                    throw new ArgumentNullException("table", "The parameter 'table' may not be null.");
                }

                var expressionHashtable = new Dictionary<long, RuleExpressionBase>(table.Rows.Count);

                LogicalRuleExpression orExpression = null;

                foreach (System.Data.DataRow row in table.Rows)
                {
                    var expression = CreateExpression(row);

                    var parentId = (row["ParentId"] != DBNull.Value) ? Tools.Convert(row["ParentId"], 0L) : 0L;
                    var annotationRootId = (row["Annotation_RootId"] != DBNull.Value) ? Tools.Convert(row["Annotation_RootId"], 0L) : 0L;

                    var fileTypeId = Tools.Convert(row["fileTypeId"], default(Guid));

                    var objectTypeId = Tools.Convert(row["ObjectTypeId"], default(Guid));

                    if (fileTypeId != default(Guid) && objectTypeId != default(Guid))
                    {
                        expression.ObjectType = fileTypes[fileTypeId].ObjectTypes[objectTypeId];
                    }

                    if (expression.CanHaveChildren)
                    {
                        expressionHashtable.Add(expression.Identity, expression);
                    }

                    if (((int)row["LogicalOperatorId"]) == 1)
                    {
                        if (orExpression == null)
                        {
                            orExpression = CreateOrExpression();
                        }

                        orExpression.Children.Add(expression);

                        expression = orExpression;

                        var sibling = expressionHashtable[parentId].GetChildren().LastOrDefault();

                        if (sibling != null)
                        {
                            expressionHashtable[parentId].RemoveChild(sibling);
                            expression.AddChild(sibling);
                        }
                    }
                    else
                    {
                        orExpression = null;
                    }

                    if (parentId != 0L)
                    {
                        expression.Parent = expressionHashtable[parentId];
                        expression.Parent.AddChild(expression);
                    }
                    else if (annotationRootId != 0L)
                    {
                        var rule = rules[annotationRootId];

                        expression.Rule = rule;
                        rule.Expression = expression;
                    }
                }
            });
        }

        /// <summary>
        /// Creates the expression.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>
        /// A Rule Expression
        /// </returns>
        private static RuleExpressionBase CreateExpression(System.Data.DataRow row)
        {
            var valueClrType = Type.GetType(row["ValueClrType"].ToString());

            if (row["PropertyName"].ToString() == "((Instance))")
            {
                return CreateLogicalRuleExpression(row);
            }

            if (valueClrType == typeof(object) || valueClrType == typeof(IEnumerable))
            {
                return CreateSetRuleExpression(row);
            }

            return CreateBinaryRuleExpression(row);
        }

        /// <summary>
        /// Creates the logical rule expression.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>
        /// A <see cref="LogicalRuleExpression" />
        /// </returns>
        private static LogicalRuleExpression CreateLogicalRuleExpression(System.Data.DataRow row)
        {
            var expression = new LogicalRuleExpression
            {
                Identity = Tools.Convert(row["Identity"], 0L),
                Operator = LogicalOperatorManager.And
            };

            return expression;
        }

        /// <summary>
        /// Creates the set rule expression.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>
        /// A <see cref="SetRuleExpression" />
        /// </returns>
        private static SetRuleExpression CreateSetRuleExpression(System.Data.DataRow row)
        {
            var expression = new SetRuleExpression
            {
                Identity = Tools.Convert(row["Identity"], 0L),
                PropertyName = row["PropertyName"].ToString(),
                Operator =
                    OperatorHelper.GetSetOperatorFromUdeOperatorCode(Tools.Convert(row["ComparisonOperatorId"], 0))
            };

            return expression;
        }

        /// <summary>
        /// Creates the binary rule expression.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>
        /// A <see cref="BinaryRuleExpression" />
        /// </returns>
        private static BinaryRuleExpression CreateBinaryRuleExpression(System.Data.DataRow row)
        {
            var expression = new BinaryRuleExpression
            {
                Identity = Tools.Convert(row["Identity"], 0L),
                PropertyName = row["PropertyName"].ToString(),
                Operator =
                    OperatorHelper.GetBinaryOperatorFromUdeOperatorCode(Tools.Convert(row["ComparisonOperatorId"], 0)),
                ClrType = Type.GetType(row["ValueClrType"].ToString())
            };

            expression.Value = (IComparable)Convert.ChangeType(row["Value"], expression.ClrType);

            return expression;
        }

        /// <summary>
        /// Creates an "Or" expression.
        /// </summary>
        /// <returns>
        /// A <see cref="LogicalRuleExpression" /> of type "Or"
        /// </returns>
        private static LogicalRuleExpression CreateOrExpression()
        {
            return new LogicalRuleExpression
            {
                Identity = idsForOrExpressions--,
                Operator = LogicalOperatorManager.GetOperator("Or")
            };
        }

        #endregion
    }
}
