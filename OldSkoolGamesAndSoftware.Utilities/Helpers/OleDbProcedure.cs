using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OldSkoolGamesAndSoftware.Data
{
    /// <summary>
    /// Provides an abstraction for the functions of an OleDbProcedure.
    /// Implements <see cref="System.IDisposable" /> and 
    /// <see cref="OldSkoolGamesAndSoftware.Data.IDataProcedure" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db")]
    public class OleDbProcedure : IDataProcedure, IDisposable
    {
        #region Fields

        private OleDbCommand _cmd = new OleDbCommand();
        private OleDbConnection _conn = new OleDbConnection();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OleDbProcedure"/> class.
        /// </summary>
        private OleDbProcedure() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OleDbProcedure"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public OleDbProcedure(string connectionString)
        {
            _conn.ConnectionString = connectionString;
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OleDbProcedure"/> class.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connectionString">The connection string.</param>
        public OleDbProcedure(string commandText, string connectionString)
            : this(commandText, connectionString, CommandType.StoredProcedure)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OleDbProcedure"/> class.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="commandType">Type of the command.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public OleDbProcedure(string commandText, string connectionString,
            CommandType commandType)
        {
            _conn.ConnectionString = connectionString;
            _cmd.CommandText = commandText;
            _cmd.CommandType = commandType;
            _cmd.Connection = _conn;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="OleDbProcedure"/> is reclaimed by garbage collection.
        /// </summary>
        ~OleDbProcedure()
        {
            Dispose(false);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Properties

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
        /// Gets the state of the connection.
        /// </summary>
        /// <value>The state of the connection.</value>
        public ConnectionState ConnectionState
        {
            get { return _conn.State; }
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
        public OleDbParameterCollection Parameters
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

        #endregion

        #region Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_conn != null)
                {
                    _conn.Dispose();
                    _conn = null;
                }

                if (_cmd != null)
                {
                    _cmd.Dispose();
                    _cmd = null;
                }
            }
        }

        /// <summary>
        /// Executes the command text and returns the number of rows affected.
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            return _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes the data reader.
        /// </summary>
        /// <returns></returns>
        public OleDbDataReader ExecuteDataReader()
        {
            if (_conn.State != ConnectionState.Open)
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
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(_cmd))
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
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(_cmd))
            {
                DataSet set = new DataSet();
                set.Locale = culture;

                adapter.Fill(set);

                return set;
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
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            return _cmd.ExecuteScalar();
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
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Close();
            }

            return _conn.State;
        }

        /// <summary>
        /// Clears the existing command text and parameters.
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
