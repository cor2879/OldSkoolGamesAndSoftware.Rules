// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorBase.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.Operators
{
    /// <summary>
    /// The public interface for Binary Evaluation operations of rule expressions.
    /// </summary>
    public abstract class OperatorBase
    {       
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates the specified expression value.
        /// </summary>
        /// <param name="fact">The fact evaluated by an operator</param>
        /// <param name="expressions">The expressions used to evaluate the operator</param>
        /// <returns>
        /// A populated <see cref="DataPointBase" /> instance if the evaluation resolves to<c>true</c>,
        /// otherwise it returns <c>null</c>.
        /// </returns>
        public abstract DataPointBase Evaluate(IFact fact, RuleExpressionBase expressions);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}