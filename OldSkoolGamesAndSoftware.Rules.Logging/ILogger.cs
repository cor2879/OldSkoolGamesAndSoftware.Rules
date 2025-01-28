// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OldSkoolGamesAndSoftware.Logging
{
    using System.Diagnostics;

    /// <summary>
    /// Defines an interface for logging.
    /// </summary>
    public interface ILogger
    {
        #region Properties

        /// <summary>
        /// Gets or sets the trace level.
        /// </summary>
        /// <value>
        /// The trace level.
        /// </value>
        TraceLevel TraceLevel { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="text">The text.</param>
        void Log(TraceLevel level, string text);

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The argument.</param>
        void Log(TraceLevel level, string format, object arg);

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        void Log(TraceLevel level, string format, object arg0, object arg1);

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        void Log(TraceLevel level, string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void Log(TraceLevel level, string format, params object[] args);

        #endregion
    }
}
