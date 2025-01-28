// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogicalOperatorManager.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.Operators
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a prototype for methods which handle logical comparison operations.
    /// </summary>
    /// <param name="fact">The fact.</param>
    /// <param name="expressions">The expressions.</param>
    /// <returns>
    /// If the evaluation finds a true result, returns a populated 
    /// <see cref="DataPointBase" /> instance corresponding to the matching
    /// fact.  Otherwise, returns <c>null</c>.
    /// </returns>
    public delegate DataPointBase LogicalComparisonDelegate(IFact fact, RuleExpressionCollection expressions);

    /// <summary>
    /// Exposes functionality for getting and handling logical operators.
    /// </summary>
    public static class LogicalOperatorManager
    {
        #region Fields

        /// <summary>
        /// The AND operator.
        /// </summary>
        public static readonly LogicalOperator And = new LogicalOperator("And", EvaluateAnd);

        /// <summary>
        /// The OR operator.
        /// </summary>
        public static readonly LogicalOperator Or = new LogicalOperator("Or", EvaluateOr);

        /// <summary>
        /// The operators
        /// </summary>
        public static readonly Dictionary<string, LogicalOperator> Operators = new Dictionary<string, LogicalOperator>
        {
            { "And", And },
            { "Or", Or }
        };

        #endregion

        #region Methods

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The LogicalOperator
        /// </returns>
        public static LogicalOperator GetOperator(string name)
        {
            return Operators[name];
        }

        /// <summary>
        /// Evaluates the and.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="expressions">The expressions.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        private static DataPointBase EvaluateAnd(IFact fact, RuleExpressionCollection expressions)
        {
            var matchIndex = new Dictionary<RuleExpressionBase, DataPointBase>();

            matchIndex.Add(expressions.First(), expressions.First().Evaluate(fact));

            foreach (var expression in expressions.Skip(1))
            {
                matchIndex.Add(expression, expression.Evaluate(fact));
            }

            if (matchIndex.Values.All(match => match != null))
            {
                return new ParentDataPoint
                {
                    Children = new ParentDataPoint.DataPointChildren(matchIndex.Values)
                };
            }

            return null;
        }

        /// <summary>
        /// Evaluates the or.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="expressions">The expressions.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        private static DataPointBase EvaluateOr(IFact fact, RuleExpressionCollection expressions)
        {
            var matchIndex = new Dictionary<RuleExpressionBase, DataPointBase>();

            matchIndex.Add(expressions.First(), expressions.First().Evaluate(fact));

            foreach (var expression in expressions.Skip(1))
            {
                matchIndex.Add(expression, expression.Evaluate(fact));
            }

            if (matchIndex.Values.Any(match => match != null))
            {
                return new ParentDataPoint
                {
                    Children = new ParentDataPoint.DataPointChildren(matchIndex.Values)
                };
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    /// Defines operators which handle logical operations (such as AND and OR).
    /// </summary>
    public class LogicalOperator
        : OperatorBase
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicalOperator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="comparer">The comparer.</param>
        internal LogicalOperator(string name, LogicalComparisonDelegate comparer)
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
        public LogicalComparisonDelegate Comparer
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
        /// <exception cref="RuleExpressionException">Invalid expression for logical evaluation.</exception>
        public override DataPointBase Evaluate(IFact fact, RuleExpressionBase expression)
        {
            var logicalExpression = expression as LogicalRuleExpression;

            if (logicalExpression == null)
            {
                throw new RuleExpressionException("Invalid expression for logical evaluation.");
            }
            
            return this.Comparer(fact, logicalExpression.Children);
        }

        #endregion
    }
}