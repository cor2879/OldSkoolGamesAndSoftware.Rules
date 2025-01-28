// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetRuleExpression.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReShaper disable once CheckNamespace
namespace OldSkoolGamesAndSoftware.Rules
{
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using OldSkoolGamesAndSoftware.Rules.Operators;

    /// <summary>
    /// Defines an expression type for describing Set relationships
    /// </summary>
    [DataContract(IsReference = true)]
    public class SetRuleExpression
        : RuleExpressionBase
    {
        #region Fields

        /// <summary>
        /// The children
        /// </summary>
        private RuleExpressionCollection children = new RuleExpressionCollection();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetRuleExpression"/> class.
        /// </summary>
        public SetRuleExpression()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetRuleExpression" /> class.
        /// </summary>
        /// <param name="root">The <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> instance which will be the root of this
        /// <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance.</param>
        /// <param name="objectType">The <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType" /> instance used to indicate
        /// what type of object this <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> represents.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="setOperator">The set operator.</param>
        public SetRuleExpression(Rule root, ObjectType objectType, string propertyName, SetOperator setOperator)
            : base(root, objectType)
        {
            this.Operator = setOperator;
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetRuleExpression" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="setOperator">The comparison operator.</param>
        public SetRuleExpression(RuleExpressionBase parent, ObjectType objectType, string propertyName, SetOperator setOperator)
            : base(parent, objectType)
        {
            this.Operator = setOperator;
            this.PropertyName = propertyName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the comparison operator which will be used to value match Data items with this
        /// IRuleExpression instance.
        /// </summary>
        /// <value>
        /// The comparison operator.
        /// </value>
        public new SetOperator Operator
        {
            get { return base.Operator as SetOperator; }
            set { base.Operator = value; }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public RuleExpressionCollection Children
        {
            get
            {
                return this.children;
            }
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
                return true;
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
            get { return this.Children.Count > 0; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public override void AddChild(RuleExpressionBase child)
        {
            this.Children.Add(child);
            child.Parent = this;
            child.Rule = this.Rule;
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void AddChild(SetRuleExpression child)
        {
            this.Children.Add(child);
            child.Parent = this;
            child.Rule = this.Rule;
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void AddChild(LogicalRuleExpression child)
        {
            this.Children.Add(child);
            child.Parent = this;
            child.Rule = this.Rule;
        }

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public override void RemoveChild(RuleExpressionBase child)
        {
            this.Children.Remove(child);
            child.Parent = null;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>
        /// The children
        /// </returns>
        public override RuleExpressionCollection GetChildren()
        {
            return this.Children;
        }

        /// <summary>
        /// Writes the first order logic string equivalent of the expression to a string builder.
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        internal override void WriteFirstOrderLogic(StringBuilder stringBuilder)
        {
            stringBuilder.Append("in ");
            stringBuilder.Append((this.Parent ?? this).ObjectType.Name);
            stringBuilder.Append('.');
            stringBuilder.Append(this.PropertyName);
            stringBuilder.Append(' ');
            stringBuilder.Append(this.Operator);
            stringBuilder.Append(" (");

            if (this.Children != null && this.Children.Any())
            {
                this.Children.First().WriteFirstOrderLogic(stringBuilder);

                foreach (var e in this.Children.Skip(1))
                {
                    stringBuilder.Append(" And ");
                    e.WriteFirstOrderLogic(stringBuilder);
                }
            }

            stringBuilder.Append(')');
        }

        #endregion
    }
}