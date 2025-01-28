// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryOperatorManager.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.Operators
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines a prototype for methods that handle binary comparison between Rule Expressions and Fact Values.
    /// </summary>
    /// <param name="factValue">The fact value.</param>
    /// <param name="expressionValue">The expression value.</param>
    /// <returns>
    /// <c>true</c> if the Comparison finds a match, otherwise <c>false</c>.
    /// </returns>
    public delegate bool BinaryComparisonDelegate(IComparable factValue, IComparable expressionValue);

    /// <summary>
    /// Exposes methods for getting and handling Binary Operators.
    /// </summary>
    public static class BinaryOperatorManager
    {
        #region Fields

        /// <summary>
        /// The equality operator
        /// </summary>
        public static readonly BinaryOperator Equal = new BinaryOperator(
            "Equal",
            (IComparable factValue, IComparable expressionValue) => 
            {
                if (factValue is string)
                {
                    return (factValue as string).Equals(expressionValue as string, StringComparison.InvariantCultureIgnoreCase);
                }

                return factValue.CompareTo(expressionValue) == 0;
            });

        /// <summary>
        /// The inequality operator
        /// </summary>
        public static readonly BinaryOperator NotEqual = new BinaryOperator(
            "NotEqual",
            (IComparable factValue, IComparable expressionValue) => 
            {
                if (factValue is string)
                {
                    return !(factValue as string).Equals(expressionValue as string, StringComparison.InvariantCultureIgnoreCase);
                }

                return factValue.CompareTo(expressionValue) != 0;
            });

        /// <summary>
        /// The greater than operator
        /// </summary>
        public static readonly BinaryOperator GreaterThan = new BinaryOperator(
            "GreaterThan",
            (IComparable factValue, IComparable expressionValue) => factValue.CompareTo(expressionValue) > 0);

        /// <summary>
        /// The greater than or equal operator
        /// </summary>
        public static readonly BinaryOperator GreaterThanOrEqual = new BinaryOperator(
            "GreaterThanOrEqual",
            (IComparable factValue, IComparable expressionValue) => factValue.CompareTo(expressionValue) > -1);

        /// <summary>
        /// The less than operator
        /// </summary>
        public static readonly BinaryOperator LessThan = new BinaryOperator(
            "LessThan",
            (IComparable factValue, IComparable expressionValue) => factValue.CompareTo(expressionValue) < 0);

        /// <summary>
        /// The less than or equal operator
        /// </summary>
        public static readonly BinaryOperator LessThanOrEqual = new BinaryOperator(
            "LessThanOrEqual",
            (IComparable factValue, IComparable expressionValue) => factValue.CompareTo(expressionValue) < 1);

        /// <summary>
        /// The Regular Expression operator
        /// </summary>
        public static readonly BinaryOperator RegExMatch = new BinaryOperator(
            "RegExMatch",
            (IComparable factValue, IComparable expressionValue) => Regex.IsMatch(factValue.ToString(), expressionValue.ToString()));

        /// <summary>
        /// The operators
        /// </summary>
        public static readonly Dictionary<string, BinaryOperator> Operators = new Dictionary<string, BinaryOperator>
        {
            { "Equal", Equal },
            { "NotEqual", NotEqual },
            { "GreaterThan", GreaterThan },
            { "GreaterThanOrEqual", GreaterThanOrEqual },
            { "LessThan", LessThan },
            { "LessThanOrEqual", LessThanOrEqual },
            { "RegExMatch", RegExMatch }
        };

        #endregion

        #region Methods

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The operator
        /// </returns>
        public static BinaryOperator GetOperator(string name)
        {
            return Operators[name];
        }

        #endregion
    }

    /// <summary>
    /// Defines an operator type for handling binary comparison operations.
    /// </summary>
    public class BinaryOperator
        : OperatorBase
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryOperator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="comparer">The comparer.</param>
        internal BinaryOperator(string name, BinaryComparisonDelegate comparer)
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
        public BinaryComparisonDelegate Comparer
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
        /// <exception cref="RuleExpressionException">Invalid expression for binary evaluation.</exception>
        public override DataPointBase Evaluate(IFact fact, RuleExpressionBase expression)
        {
            var binaryExpression = expression as BinaryRuleExpression;

            if (binaryExpression == null)
            {
                throw new RuleExpressionException("Invalid expression for binary evaluation.");
            }

            var property = fact.GetType().GetProperty(binaryExpression.PropertyName);

            var value = property.GetValue(fact, null) as IComparable;

            if (this.Comparer(value, binaryExpression.Value))
            {
                return DataPointBase.CreateDataPoint(expression.PropertyName, value);
            }

            return null;
        }

        #endregion
    }
}