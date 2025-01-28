// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleLogger.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System.Diagnostics;
    using OldSkoolGamesAndSoftware.Logging;

    /// <summary>
    /// Exposes methods used for logging within the UDE Rules framework.
    /// </summary>
    public static class RuleLogger
    {
        /// <summary>
        /// The <see cref="ILogger" /> instance
        /// </summary>
        private static ILogger instance = null;

        #region Methods

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ILogger Instance 
        {
            get
            {
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Logs the specified message and <see cref="TraceLevel" />.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="text">The text.</param>
        internal static void Log(TraceLevel level, string text)
        {
            if (Instance != null)
            {
                Instance.Log(level, text);
            }
        }

        /// <summary>
        /// Logs the specified message and <see cref="TraceLevel" />.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The argument.</param>
        internal static void Log(TraceLevel level, string format, object arg)
        {
            if (Instance != null)
            {
                Instance.Log(level, format, arg);
            }
        }

        /// <summary>
        /// Logs the specified message and <see cref="TraceLevel" />.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        internal static void Log(TraceLevel level, string format, object arg0, object arg1)
        {
            if (Instance != null)
            {
                Instance.Log(level, format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs the specified message and <see cref="TraceLevel" />.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        internal static void Log(TraceLevel level, string format, object arg0, object arg1, object arg2)
        {
            if (Instance != null)
            {
                Instance.Log(level, format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs the specified message and <see cref="TraceLevel" />.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        internal static void Log(TraceLevel level, string format, params object[] args)
        {
            if (Instance != null)
            {
                Instance.Log(level, format, args);
            }
        }

        #endregion
    }
}
