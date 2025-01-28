// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Micrsoft Corporation">
//   Copyright © 2024 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OldSkoolGamesAndSoftware.Utilities.Extensions
{
    using System;
    using System.Security;

    /// <summary>
    /// Contains extension methods for .Net types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Constructs a <see cref="System.Security.SecureString"/> from the supplied string.
        /// </summary>
        /// <param name="unsecuredString">Plain text string to secure.</param>
        /// <returns><see cref="System.Security.SecureString"/> constructed from plain text string.</returns>
        public static SecureString ToSecureString(this String unsecuredString)
        {
            var secureString = new SecureString();

            foreach (char c in unsecuredString)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();

            return secureString;
        }
    }
}
