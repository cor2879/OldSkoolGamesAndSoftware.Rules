// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLogger.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OldSkoolGamesAndSoftware.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Implementation of <see cref="ILogger" /> which writes all log output to the Console.
    /// </summary>
    public class ConsoleLogger
        : ILogger
    {
        #region Fields

        /// <summary>
        /// The trace level
        /// </summary>
        private TraceLevel traceLevel = TraceLevel.Verbose;

        #endregion

        #region ILogger Members

        /// <summary>
        /// Gets or sets the trace level.
        /// </summary>
        /// <value>
        /// The trace level.
        /// </value>
        public TraceLevel TraceLevel 
        { 
            get
            {
                return this.traceLevel; 
            }
            
            set
            {
                this.traceLevel = value;
            }
        }

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="text">The text.</param>
        public void Log(TraceLevel level, string text)
        {
            if (level <= this.TraceLevel)
            {
                Console.WriteLine("[{0}] - {1} - {2}", DateTime.Now.ToString("o"), level, text);
            }
        }

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The argument.</param>
        public void Log(TraceLevel level, string format, object arg)
        {
            if (level <= this.TraceLevel)
            {
                Console.WriteLine("[{0}] - {1} - {2}", DateTime.Now.ToString("o"), level, string.Format(format, arg));
            }
        }

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void Log(TraceLevel level, string format, object arg0, object arg1)
        {
            if (level <= this.TraceLevel)
            {
                Console.WriteLine("[{0}] - {1} - {2}", DateTime.Now.ToString("o"), level, string.Format(format, arg0, arg1));
            }
        }

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void Log(TraceLevel level, string format, object arg0, object arg1, object arg2)
        {
            if (level <= this.TraceLevel)
            {
                Console.WriteLine("[{0}] - {1} - {2}", DateTime.Now.ToString("o"), level, string.Format(format, arg0, arg1, arg2));
            }
        }

        /// <summary>
        /// Logs the text output and the specified TraceLevel
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void Log(TraceLevel level, string format, params object[] args)
        {
            if (level <= this.TraceLevel)
            {
                Console.WriteLine("[{0}] - {1} - {2}", DateTime.Now.ToString("o"), level, string.Format(format, args));
            }
        }

        #endregion
    }
}
