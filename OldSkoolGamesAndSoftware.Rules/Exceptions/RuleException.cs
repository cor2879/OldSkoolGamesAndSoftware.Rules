// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleException.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a base class for Exceptions that occur during internal processing of the
    /// Rule object model.
    /// </summary>
    [Serializable]
    public class RuleException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleException"/> class.
        /// </summary>
        public RuleException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RuleException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RuleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds 
        /// the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="dataPoint">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains 
        /// data contextual information about the source or destination.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). 
        /// </exception>
        protected RuleException(SerializationInfo info, StreamingContext dataPoint)
            : base(info, dataPoint)
        {
        }
#endif

        #endregion
    }

    /// <summary>
    /// Defines a class for Exceptions that occur as a direct result processing Rule Items.
    /// </summary>
    [Serializable]
    public class RuleExpressionException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionException"/> class.
        /// </summary>
        public RuleExpressionException()
            : base()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RuleExpressionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RuleExpressionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="dataPoint">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains data contextual 
        /// information about the source or destination.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null. 
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). 
        /// </exception>
        protected RuleExpressionException(SerializationInfo info, StreamingContext dataPoint)
            : base(info, dataPoint)
        {
        }
#endif
        #endregion
    }
}