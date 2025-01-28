// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rule.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using OldSkoolGamesAndSoftware.Logging;

    /// <summary>
    /// Defines the root data item which represent a single rule.
    /// </summary>
    [DataContract(IsReference = true)]
    public class Rule 
    {
        #region Fields

        /// <summary>
        /// The error trace level
        /// </summary>
        private const TraceLevel Error = TraceLevel.Error;

        /// <summary>
        /// The verbose trace level
        /// </summary>
        private const TraceLevel Verbose = TraceLevel.Verbose;

        /// <summary>
        /// The information trace level
        /// </summary>
        private const TraceLevel Info = TraceLevel.Info;

        /// <summary>
        /// The comments
        /// </summary>
        private RuleCommentCollection comments = new RuleCommentCollection();

        /// <summary>
        /// The expression
        /// </summary>
        private RuleExpressionBase expression;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        public Rule() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public Rule(string text)
        {
            this.AddComment(text);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>The identity.</value>
        [DataMember]
        public Guid Identity { get; set; }

        /// <summary>
        /// Gets or sets the solution source identifier.
        /// </summary>
        /// <value>
        /// The solution source identifier.
        /// </value>
        public Guid SolutionSourceId { get; set; }

        /// <summary>
        /// Gets or sets the legacy identifier.
        /// </summary>
        /// <value>
        /// The legacy identifier.
        /// </value>
        public long LegacyId { get; set; }

        /// <summary>
        /// Gets or sets the user id of the user who created the rule.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [DataMember]
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the rule items.
        /// </summary>
        /// <value>The rule items.</value>
        [DataMember]
        public RuleExpressionBase Expression
        {
            get { return this.expression; }

            set
            {
                this.expression = value;
                this.expression.Rule = this;
            }
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        [DataMember]
        public RuleCommentCollection Comments
        {
            get { return this.comments; }
            internal set { this.comments = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Rule"/> is inactive.
        /// </summary>
        /// <value>
        /// <c>true</c> if inactive; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool Inactive
        {
            get;
            set;
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Rule lhs, object rhs)
        {
            if (((object)lhs) == null)
            {
                return rhs == null;
            }

            return lhs.Equals(rhs as Rule);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Rule lhs, object rhs)
        {
            if (((object)lhs) == null)
            {
                return rhs != null;
            }

            return !lhs.Equals(rhs as Rule);
        }

        /// <summary>
        /// Adds a new comment.
        /// </summary>
        /// <param name="text">The text.</param>
        public void AddComment(string text)
        {
            this.comments.Add(new RuleComment(this, text));
        }

        /// <summary>
        /// Evaluates the specified fact.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns>
        /// If the evaluation finds a true result, returns a populated 
        /// <see cref="DataPointBase" /> instance corresponding to the matching
        /// fact.  Otherwise, returns <c>null</c>.
        /// </returns>
        public DataPointBase Evaluate(IFact fact)
        {
            RuleLogger.Log(
                Verbose, 
                "Rule {0} beginning evaluation against fact {1} of type {2}", 
                this.LegacyId, 
                fact.Identity, 
                fact.ObjectType);

            var dataPoint = this.Expression.Evaluate(fact);

            RuleLogger.Log(
                Verbose,
                "Evaluation returned as {0}",
                (dataPoint != null) ? dataPoint.ToString() : "(null)");

            return dataPoint;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Rule);
        }

        /// <summary>
        /// Determines whether the specified <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> is equal to this instance; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Rule other)
        {
            if (((object)other) == null)
            {
                return false;
            }

            return this.GetType().Equals(other.GetType()) && this.Identity.Equals(other.Identity);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Identity.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this.Expression == null)
            {
                return "(null)";
            }

            return this.Expression.ToString();
        }

        #endregion
    }
}
