using System;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines an EventArg class which can be used to convey
    /// meaningful data generically when the state of an single
    /// value is changed.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    public class ValueChangedEventArgs<TData> 
        : EventArgs
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueChangedEventArgs&lt;TData&gt;"/> class.
        /// </summary>
        public ValueChangedEventArgs()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueChangedEventArgs&lt;TData&gt;"/> class.
        /// </summary>
        /// <param name="oldValue">The previous value.</param>
        /// <param name="newValue">The new value.</param>
        public ValueChangedEventArgs(TData oldValue, TData newValue)
            : base()
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the old value.
        /// </summary>
        public TData OldValue { get; private set; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        public TData NewValue { get; private set; }

        #endregion
    }
}