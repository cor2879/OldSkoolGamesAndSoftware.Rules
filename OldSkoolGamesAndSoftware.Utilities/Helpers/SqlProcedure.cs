using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

using OldSkoolGamesAndSoftware.Utilities.Exceptions;

namespace OldSkoolGamesAndSoftware.Data
{
    /// <summary>
    /// Provides an abstraction for the functions of a SqlProcedure.
    /// Implements <see cref="System.IDisposable" /> and 
    /// <see cref="OldSkoolGamesAndSoftware.Data.IDataProcedure" />.
    /// </summary>
    public sealed class SqlProcedure : IDataProcedure, IDisposable
    {
        #region Fields

        private SqlCommand _cmd = new SqlCommand();
        private SqlConnection _conn = new SqlConnection();
        private String _transactionId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedure"/> class.
        /// </summary>
        private SqlProcedure()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedure"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlProcedure(string connectionString)
        {
            _conn.ConnectionString = connectionString;
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Connection = _conn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedure"/> class.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connectionString">The Connection String with which to connect.</param>
        public SqlProcedure(string commandText, string connectionString) 
            : this(commandText, connectionString, CommandType.StoredProcedure)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlProcedure" /> class.
        /// </summary>
        /// <param name="commandText">
        ///     The command text.
        /// </param>
        /// <param name="connectionString">
        ///     The Connection String with which to connect.
        /// </param>
        /// <param name="commandType">
        ///     The command type of the Sql Command.  The default value is
        ///     CommandType.StoredProcedure.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public SqlProcedure(string commandText, string connectionString, CommandType commandType)
        {
            _conn.ConnectionString = connectionString;
            _cmd.CommandText = commandText;
            _cmd.CommandType = commandType;
            _cmd.Connection = _conn;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SqlProcedure"/> is reclaimed by garbage collection.
        /// </summary>
        ~SqlProcedure()
        {
            Dispose(false);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, 
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the state of the connection.
        /// </summary>
        /// <value>The state of the connection.</value>
        public ConnectionState ConnectionState
        {
            get { return _conn.State; }
        }

        /// <summary>
        /// Gets or sets the wait time before terminating the attempt to execute a command
        /// and generating an error.
        /// </summary>
        /// <value>
        /// The time in seconds to wait for the command to execute. The default is 30
        /// seconds.
        /// </value>
        public Int32 CommandTimeout
        {
            get { return _cmd.CommandTimeout; }
            set { _cmd.CommandTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get { return _conn.ConnectionString; }
            set { _conn.ConnectionString = value; }
        }

        /// <summary>
        /// Get Database ServerName
        /// </summary>
        public string DataSource
        {
            get { return _conn.DataSource; }
        }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>The command text.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public string CommandText
        {
            get { return _cmd.CommandText; }
            set { _cmd.CommandText = value; }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public SqlParameterCollection Parameters
        {
            get { return _cmd.Parameters; }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        DbParameterCollection IDataProcedure.Parameters
        {
            get { return _cmd.Parameters; }
        }

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>The type of the command.</value>
        public CommandType CommandType
        {
            get { return _cmd.CommandType; }
            set { _cmd.CommandType = value; }
        }

        /// <summary>
        /// Gets a boolean value indicating whether or not this
        /// <see cref="OldSkoolGamesAndSoftware.Data.SqlProcedure" />
        /// instance is operating in a transactional mode.  If so,
        /// then all commands executed will occur using a database
        /// transaction which must be either committed or rolled back
        /// using the CommitTransaction or RollbackTransaction methods of the SqlProcedure
        /// class, respectively.  Calling either the CommitTransaction or
        /// RollbackTransaction methods when IsTransactional == <c>false</c>
        /// will result in an exception.  To make this instance 
        /// transactional, call the BeginTransaction method.
        /// </summary>
        public bool IsTransactional
        {
            get { return _cmd.Transaction != null; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; 
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!(_conn == null))
                {
                    _conn.Dispose();
                    _conn = null;
                }

                if (!(_cmd == null))
                {
                    _cmd.Dispose();
                    _cmd = null;
                }
            }
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            _transactionId = Guid.NewGuid().ToString().Substring(0, 32);
            _cmd.Transaction = _conn.BeginTransaction(_transactionId);
        }

        /// <summary>
        /// Starts a database transaction with the specified name.
        /// </summary>
        /// <param name="name">The name of the transaction.</param>
        public void BeginTransaction(string name)
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            _transactionId = name.Substring(0, Math.Min(name.Length, 32));
            _cmd.Transaction = _conn.BeginTransaction(_transactionId);
        }

        /// <summary>
        /// Starts a database transaction with the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            _transactionId = Guid.NewGuid().ToString().Substring(0, 32);
            _cmd.Transaction = _conn.BeginTransaction(isolationLevel, _transactionId);
        }

        /// <summary>
        /// Starts a database transaction with the specified isolation level and name.
        /// </summary>
        /// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
        /// <param name="name">The name of the transaction.</param>
        public void BeginTransaction(IsolationLevel isolationLevel, string name)
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            _transactionId = name.Substring(0, Math.Min(name.Length, 32));
            _cmd.Transaction = _conn.BeginTransaction(isolationLevel, _transactionId);
        }

        /// <summary>
        /// Commits a transaction that has been created by calling the
        /// BeginTransaction method of this instance.
        /// </summary>
        /// <exception cref="OldSkoolGamesAndSoftware.Utilities.Exceptions.SqlProcedureException">
        /// Thrown if the BeginTransaction method has not been called, or
        /// if the IsTransactional property is <c>false</c>.
        /// </exception>
        public void CommitTransaction()
        {
            if (!IsTransactional)
            {
                throw new SqlProcedureException("An attempt was made to commit a transaction when no trancation was created.");
            }

            _cmd.Transaction.Commit();

            if (_cmd.Transaction != null)
            {
                _cmd.Transaction.Dispose();
                _cmd.Transaction = null;
            }
        }

        /// <summary>
        /// Rolls back a transaction that has been created by calling the.
        /// BeginTransaction method of this instance.  After this method is
        /// called, the this <see cref="OldSkoolGamesAndSoftware.Data.SqlProcedure" />
        /// instance will no longer be in transactional mode.
        /// </summary>
        /// 
        public void RollbackTransaction()
        {
            if (!IsTransactional)
            {
                throw new SqlProcedureException("An attempt was made to roll back a transaction when no transaction was created.");
            }

            _cmd.Transaction.Rollback(_transactionId);

            if (_cmd.Transaction != null)
            {
                _cmd.Transaction.Dispose();
                _cmd.Transaction = null;
            }
        }

        /// <summary>
        /// Attempts to execute the command text in the form of a scalar function.
        /// </summary>
        /// <returns>
        /// Returns the result of the scalar operation.
        /// </returns>
        public object ExecuteScalar()
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            return _cmd.ExecuteScalar();
        }

        /// <summary>
        /// Executes the Sql Command and returns the number of rows affected.
        /// </summary>
        public int ExecuteNonQuery()
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            return _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes the Sql Command and returns the resulting SqlDataReader.
        /// </summary>
        public SqlDataReader ExecuteDataReader()
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            return _cmd.ExecuteReader();
        }

        /// <summary>
        /// Executes the command text and returns the resulting data in a
        /// forward-only, as-available data structure.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="System.Data.IDataReader"/> object.
        /// </returns>
        IDataReader IDataProcedure.ExecuteDataReader()
        {
            return ExecuteDataReader();
        }

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="System.Data.DataTable"/>
        /// </returns>
        public DataTable ExecuteDataTable()
        {
            return ExecuteDataTable(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable"/>.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// Returns a <see cref="System.Data.DataTable"/>
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataTable ExecuteDataTable(CultureInfo culture)
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            using (SqlDataAdapter adapter = new SqlDataAdapter(_cmd))
            {
                DataTable table = new DataTable();
                table.Locale = culture;

                adapter.Fill(table);

                return table;
            }
        }

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataSet"/>
        /// </summary>
        /// <returns>
        /// Returns a <see cref="System.Data.DataSet"/>
        /// </returns>
        public DataSet ExecuteDataSet()
        {
            return ExecuteDataSet(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataSet"/>
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// Returns a <see cref="System.Data.DataSet"/>
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataSet ExecuteDataSet(CultureInfo culture)
        {
            if (!(_conn.State == ConnectionState.Open))
            {
                _conn.Open();
            }

            using (SqlDataAdapter adapter = new SqlDataAdapter(_cmd))
            {
                // Create the DataSet
                DataSet set = new DataSet();
                set.Locale = culture;

                adapter.Fill(set);

                return set;
            }
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <returns></returns>
        public ConnectionState OpenConnection()
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            return _conn.State;
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <returns></returns>
        public ConnectionState CloseConnection()
        {
            if (_conn.State != ConnectionState.Closed)
            {
                _conn.Close();
            }

            return _conn.State;
        }

        /// <summary>
        /// Clears the command text and any associated parameters.
        /// Does not close any existing connections.
        /// </summary>
        public void Clear()
        {
            _cmd.CommandText = null;
            _cmd.Parameters.Clear();
        }

        #endregion
    }
}