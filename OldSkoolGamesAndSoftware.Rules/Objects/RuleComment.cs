// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleComment.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Describes a single comment associated with an Rule object
    /// </summary>
    [DataContract]
    public class RuleComment
    {
        #region Fields

        /// <summary>
        /// The rule
        /// </summary>
        private Rule rule;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleComment"/> class.
        /// </summary>
        public RuleComment()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleComment"/> class.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="text">The text.</param>
        public RuleComment(Rule rule, string text)
        {
            this.Rule = rule;
            this.Text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>The identity.</value>
        [DataMember]
        public long Identity { get; set; }

        /// <summary>
        /// Gets the rule.
        /// </summary>
        /// <value>The rule.</value>
        [DataMember]
        public Rule Rule
        {
            get { return this.rule; }
            internal set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "The property 'Rule' may not be null.");
                }

                this.rule = value;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [DataMember]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        [DataMember]
        public DateTimeOffset DateCreated { get; set; }

        #endregion
    }
}
