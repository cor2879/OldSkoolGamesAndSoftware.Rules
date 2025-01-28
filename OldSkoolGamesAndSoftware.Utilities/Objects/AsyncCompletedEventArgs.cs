using System;
using System.ComponentModel;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Provides a generic event handler for Asyncronous events
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class AsyncCompletedEventArgs<TResult>
        : AsyncCompletedEventArgs
    {
        #region Fields

        private TResult _result;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCompletedEventArgs&lt;TResult&gt;"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancelled">if set to <c>true</c> the event action will be cancelled.</param>
        /// <param name="userState">The userState.</param>
        public AsyncCompletedEventArgs(TResult result, Exception error, bool cancelled, Object userState)
            : base(error, cancelled, userState)
        {
            Result = result;
            if (error != null)
            {
                ErrorMessage = error.Message;
                IsTimeout = error is TimeoutException;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public TResult Result
        {
            get { return _result; }
            private set { _result = value; }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is timeout.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is timeout; otherwise, <c>false</c>.
        /// </value>
        public bool IsTimeout { get; set; }

        #endregion
    }
}