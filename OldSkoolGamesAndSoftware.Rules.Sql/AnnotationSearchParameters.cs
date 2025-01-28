// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnnotationSearchParameters.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OldSkoolGamesAndSoftware.Rules.Sql
{
    using System;

    /// <summary>
    /// Defines a data structure which contains various parameters that may be passed to
    /// a SQL database in order to search for Annotation Rules.
    /// </summary>
    public class AnnotationSearchParameters
    {
        #region Properties

        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>
        /// The type of the file.
        /// </value>
        public Guid FileType { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        public Guid ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        /// <value>
        /// The name of the domain.
        /// </value>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the name of the sam account.
        /// </summary>
        /// <value>
        /// The name of the sam account.
        /// </value>
        public string SamAccountName { get; set; }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public long? Identity { get; set; }

        /// <summary>
        /// Gets or sets the solution source identifier.
        /// </summary>
        /// <value>
        /// The solution source identifier.
        /// </value>
        public Guid? SolutionSourceId { get; set; }

        /// <summary>
        /// Gets or sets the inactive.
        /// </summary>
        /// <value>
        /// The inactive.
        /// </value>
        public bool? Inactive { get; set; }

        #endregion
    }
}