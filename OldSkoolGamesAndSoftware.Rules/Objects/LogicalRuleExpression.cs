// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogicalRuleExpression.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using OldSkoolGamesAndSoftware.Rules.Operators;

    /// <summary>
    /// Defines a Rule Expression that represents a logical statement (such as AND and OR).
    /// </summary>
    [DataContract(IsReference = true)]
    public class LogicalRuleExpression
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
        /// Initializes a new instance of the <see cref="LogicalRuleExpression"/> class.
        /// </summary>
        public LogicalRuleExpression() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicalRuleExpression" /> class.
        /// </summary>
        /// <param name="rule">The <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> instance which will be the root of this
        /// <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance.</param>
        /// <param name="objectType">The <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType" /> instance used to indicate
        /// what type of object this <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> represents.</param>
        /// <param name="logicalOperator">The logical operator.</param>
        public LogicalRuleExpression(Rule rule, ObjectType objectType, LogicalOperator logicalOperator)
            : base(rule, objectType)
        {
            this.Operator = logicalOperator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicalRuleExpression" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="logicalOperator">The logical operator.</param>
        public LogicalRuleExpression(RuleExpressionBase parent, ObjectType objectType, LogicalOperator logicalOperator)
            : base(parent, objectType)
        {
            this.Operator = logicalOperator;
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
        public new LogicalOperator Operator
        {
            get { return base.Operator as LogicalOperator; }
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
            get { return this.children; }
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
            get
            {
                return this.Children.Count > 0;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates the specified fact.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        public override DataPointBase Evaluate(IFact fact)
        {
            var dataPoint = base.Evaluate(fact) as ParentDataPoint;

            if (dataPoint != null)
            {
                dataPoint.Name = this.ObjectType.Name;
                dataPoint.Children.Add(new DataPoint() { Name = "Identity", Value = fact.Identity });
            }

            return dataPoint;
        }

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
        public void AddChild(LogicalRuleExpression child)
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
        /// Removes the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public override void RemoveChild(RuleExpressionBase child)
        {
            this.Children.Remove(child);
            child.Parent = null;
        }

        /// <summary>
        /// Writes the first order logic string equivalent of the expression to a string builder.
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        internal override void WriteFirstOrderLogic(StringBuilder stringBuilder)
        {
            stringBuilder.Append('(');

            if (this.Children != null && this.Children.Any())
            {
                this.Children.First().WriteFirstOrderLogic(stringBuilder);

                foreach (var e in this.Children.Skip(1))
                {
                    stringBuilder.Append(' ');
                    stringBuilder.Append(this.Operator);
                    stringBuilder.Append(' ');
                    e.WriteFirstOrderLogic(stringBuilder);
                }
            }

            stringBuilder.Append(')');
        }

        #endregion
    }
}