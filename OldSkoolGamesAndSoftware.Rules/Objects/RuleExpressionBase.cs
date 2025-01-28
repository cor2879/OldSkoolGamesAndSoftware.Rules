// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleExpressionBase.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Text;

    using OldSkoolGamesAndSoftware.Rules.Operators;

    /// <summary>
    /// Provides a base type for RuleExpressions.  Each RuleExpression describes a single attribute about a data item
    /// that has been annotated.
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType("GetKnownTypes")]
    public abstract class RuleExpressionBase
    {
        #region Fields

        /// <summary>
        /// The error
        /// </summary>
        private const TraceLevel Error = TraceLevel.Error;

        /// <summary>
        /// The warning
        /// </summary>
        private const TraceLevel Warning = TraceLevel.Warning;

        /// <summary>
        /// The information
        /// </summary>
        private const TraceLevel Info = TraceLevel.Info;

        /// <summary>
        /// The rule
        /// </summary>
        private Rule rule;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionBase"/> class.
        /// </summary>
        public RuleExpressionBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionBase"/> class.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="objectType">Type of the object.</param>
        protected RuleExpressionBase(Rule rule, ObjectType objectType)
        {
            this.Rule = rule;
            this.ObjectType = objectType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="objectType">Type of the object.</param>
        protected RuleExpressionBase(RuleExpressionBase parent, ObjectType objectType)
        {
            this.Parent = parent;
            this.ObjectType = objectType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identity of the Rule Item.
        /// </summary>
        /// <value>The identity.</value>
        [DataMember]
        public long Identity { get; set; }

        /// <summary>
        /// Gets or sets the parent IRuleExpression instance.
        /// </summary>
        /// <value>The parent.</value>
        [DataMember]
        public RuleExpressionBase Parent { get; set; }

        /// <summary>
        /// Gets or sets the comparison operator which will be used to value match Data items with this
        /// IRuleExpression instance.
        /// </summary>
        /// <value>The comparison operator.</value>
        [DataMember]
        public OperatorBase Operator { get; set; }

        /// <summary>
        /// Gets or sets the Rule instance which represents the discrete Rule to which
        /// this IRuleExpression is tied.
        /// </summary>
        /// <value>The rule.</value>
        [DataMember]
        public Rule Rule
        {
            get
            {
                return this.rule ?? (this.Parent != null ? this.Parent.Rule : null);
            }

            set
            {
                this.rule = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        [DataMember]
        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        [DataMember]
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        public abstract bool HasChildren { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can have children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can have children; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CanHaveChildren { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>
        /// The children
        /// </returns>
        public abstract RuleExpressionCollection GetChildren();

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public abstract void AddChild(RuleExpressionBase child);

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public abstract void RemoveChild(RuleExpressionBase child);

        /// <summary>
        /// Evaluates the specified fact.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        public virtual DataPointBase Evaluate(IFact fact)
        {
            var dataPoint = this.Operator.Evaluate(fact, this);

            return dataPoint;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            this.WriteFirstOrderLogic(stringBuilder);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the known derived types of this base class in order to facilitate communication over
        /// WCF.
        /// </summary>
        /// <returns>
        /// The known types
        /// </returns>
        internal static Type[] GetKnownTypes()
        {
            return new[]
            {
                typeof(SetRuleExpression),
                typeof(LogicalRuleExpression),
                typeof(BinaryRuleExpression)
            };
        }

        /// <summary>
        /// Writes the first order logic string equivalent of the expression to a string builder.
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        internal abstract void WriteFirstOrderLogic(StringBuilder stringBuilder);

        #endregion
    }
}
