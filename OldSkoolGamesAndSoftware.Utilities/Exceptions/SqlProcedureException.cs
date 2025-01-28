using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OldSkoolGamesAndSoftware.Utilities.Exceptions
{
    class SqlProcedureException
        : DbException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedureException"/> class.
        /// </summary>
        public SqlProcedureException()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SqlProcedureException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        public SqlProcedureException(string message, int errorCode)
            : base(message, errorCode)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedureException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SqlProcedureException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedureException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected SqlProcedureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }
}
