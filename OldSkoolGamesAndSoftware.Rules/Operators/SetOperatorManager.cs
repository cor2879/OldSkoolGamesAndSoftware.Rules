// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetOperatorManager.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.Operators
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a prototype for methods which handle Set evaluation operations.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <param name="collection">The collection.</param>
    /// <returns>
    /// If the evaluation finds a true result, returns a populated 
    /// <see cref="DataPointBase" /> instance corresponding to the matching
    /// fact.  Otherwise, returns <c>null</c>.
    /// </returns>
    public delegate DataPointBase SetDelegate(SetRuleExpression expression, IList collection);

    /// <summary>
    /// Exposes functionality for getting and managing Set Operators.
    /// </summary>
    public static class SetOperatorManager
    {
        #region Fields

        /// <summary>
        /// The exists operator
        /// </summary>
        public static readonly SetOperator Exists = new SetOperator("Exists", EvaluateExists);

        /// <summary>
        /// The operators
        /// </summary>
        public static readonly Dictionary<string, SetOperator> Operators = new Dictionary<string, SetOperator>
        {
            { "Exists", Exists }
        };

        #endregion

        #region Methods

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The SetOperator
        /// </returns>
        public static SetOperator GetOperator(string name)
        {
            return Operators[name];
        }

        /// <summary>
        /// Evaluates the exists.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        private static DataPointBase EvaluateExists(SetRuleExpression expression, IList collection)
        {
            var matchIndex = new Dictionary<RuleExpressionBase, DataPointBase>();

            foreach (var child in expression.Children)
            {
                matchIndex.Add(child, null);

                int i = 0;

                while (matchIndex[child] == null && i < collection.Count)
                {
                    matchIndex[child] = child.Evaluate(collection[i++] as IFact);
                }
            } 

            if (matchIndex.Values.All(match => match != null))
            {
                return new ParentDataPoint() 
                {
                    Name = expression.PropertyName,
                    Children = new ParentDataPoint.DataPointChildren(matchIndex.Values)
                };
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    /// Defines functionality for operators which handle Set operations (such as EXISTS).
    /// </summary>
    public class SetOperator
        : OperatorBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOperator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="comparer">The comparer.</param>
        internal SetOperator(string name, SetDelegate comparer)
        {
            this.Name = name;
            this.Comparer = comparer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        /// <value>
        /// The comparer.
        /// </value>
        public SetDelegate Comparer
        {
            get; private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates the specified fact.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        /// <exception cref="RuleExpressionException">Invalid expression type for set evaluation.</exception>
        public override DataPointBase Evaluate(IFact fact, RuleExpressionBase expression)
        {
            var setExpression = expression as SetRuleExpression;

            if (setExpression == null)
            {
                throw new RuleExpressionException("Invalid expression type for set evaluation.");
            }

            var property = fact.GetType().GetProperty(setExpression.PropertyName);

            if (property == null)
            {
                return null;
            }

            return this.Comparer(setExpression, property.GetValue(fact, null) as IList);
        }

        #endregion
    }
}