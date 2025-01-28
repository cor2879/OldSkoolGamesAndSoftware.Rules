// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFactFile.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;

    /// <summary>
    /// Defines an interface for objects that represent a File
    /// that is compatible for rule.
    /// </summary>
    public interface IFactFile
    {
        #region Properties

        /// <summary>
        /// Gets the request id.
        /// </summary>
        /// <value>The request id.</value>
        Guid RequestId { get; }

        /// <summary>
        /// Gets the file unique identifier.
        /// </summary>
        /// <value>
        /// The file unique identifier.
        /// </value>
        Guid FileGuid { get; }

        /// <summary>
        /// Gets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        ModelType ModelType { get; }

        #endregion
    }
}