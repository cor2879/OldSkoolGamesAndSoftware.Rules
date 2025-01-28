using System;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines an EventArg class which can be used to convey
    /// meaningful data generically.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public class EventArgs<TData> 
        : EventArgs
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;TData&gt;"/> class.
        /// </summary>
        public EventArgs()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;TData&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public EventArgs(TData data)
            : base()
        {
            Data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public TData Data { get; protected set; }

        #endregion
    }
}