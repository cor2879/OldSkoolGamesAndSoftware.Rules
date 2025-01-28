// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFact.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    /// <summary>
    /// Defines a contract for Objects which may be annotated against using
    /// the Rule API.  Objects which will may be annotated must
    /// implement this interface.
    /// </summary>
    public interface IFact
    {
        #region Properties

        /// <summary>
        /// Gets the unique Identity of the object to be annotated.
        /// </summary>
        long Identity { get; }

        /// <summary>
        /// Gets the <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType" /> of the
        /// object instance that is being annotated.
        /// </summary>
        ObjectType ObjectType { get; }

        /// <summary>
        /// Gets the File with which this instance is associated.
        /// </summary>
        IFactFile File { get; }

        #endregion
    }
}