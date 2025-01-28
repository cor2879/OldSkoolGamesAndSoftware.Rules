// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryRuleExpression.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Runtime.Serialization;
    using System.Text;

    using OldSkoolGamesAndSoftware.Rules.Operators;

    /// <summary>
    /// Defines an base class for RuleExpressions.
    /// </summary>
    [DataContract(IsReference = true)]
    public class BinaryRuleExpression
        : RuleExpressionBase
    {
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryRuleExpression"/> class.
        /// </summary>
        public BinaryRuleExpression() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryRuleExpression" /> class.
        /// </summary>
        /// <param name="rule">The <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> instance which will be the root of this
        /// <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance.</param>
        /// <param name="objectType">The <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType" /> instance used to indicate
        /// what type of object this <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> represents.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="comparisonOperator">The comparison operator.</param>
        /// <param name="value">The value.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "It's ok here")]
        internal BinaryRuleExpression(Rule rule, ObjectType objectType, string propertyName, BinaryOperator comparisonOperator, IComparable value)
            : base(rule, objectType)
        {
            this.PropertyName = propertyName;
            this.Operator = comparisonOperator;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryRuleExpression" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="comparisonOperator">The comparison operator.</param>
        /// <param name="value">The value.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "It's ok here")]
        internal BinaryRuleExpression(RuleExpressionBase parent, ObjectType objectType, string propertyName, BinaryOperator comparisonOperator, IComparable value)
            : base(parent, objectType)
        {
            this.PropertyName = propertyName;
            this.Operator = comparisonOperator;
            this.Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [DataMember]
        public IComparable Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the color.
        /// </summary>
        /// <value>
        /// The type of the color.
        /// </value>
        public Type ClrType { get; set; }

        /// <summary>
        /// Gets or sets the comparison operator which will be used to value match Data items with this
        /// IRuleExpression instance.
        /// </summary>
        /// <value>
        /// The comparison operator.
        /// </value>
        public new BinaryOperator Operator
        {
            get { return base.Operator as BinaryOperator; }
            set { base.Operator = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can have children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can have children; otherwise, <c>false</c>.
        /// </value>
        public override bool CanHaveChildren
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        public override bool HasChildren
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <exception cref="System.NotImplementedException">
        /// Not Implemented
        /// </exception>
        public override void AddChild(RuleExpressionBase child)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <exception cref="System.NotImplementedException">
        /// Not Implemented
        /// </exception>
        public override void RemoveChild(RuleExpressionBase child)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>
        /// Returns <c>null</c> because a BinaryRuleExpression does not have children.
        /// </returns>
        public override RuleExpressionCollection GetChildren()
        {
            return null;
        }

        /// <summary>
        /// Writes the first order logic string equivalent of the expression to a string builder.
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        internal override void WriteFirstOrderLogic(StringBuilder stringBuilder)
        {
            stringBuilder.Append(this.PropertyName);
            stringBuilder.Append(' ');
            stringBuilder.Append(this.Operator);
            stringBuilder.Append(' ');

            if (this.Value == null)
            {
                stringBuilder.Append("(null)");
            }
            else if (this.Value.GetType() == typeof(string))
            {
                stringBuilder.Append("\"");
                stringBuilder.Append(this.Value);
                stringBuilder.Append("\"");
            }
            else
            {
                stringBuilder.Append(this.Value);
            }
        }

        #endregion
    }
}