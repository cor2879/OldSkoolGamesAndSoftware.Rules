using System;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace OldSkoolGamesAndSoftware.Data
{
    /// <summary>
    /// Defines an implementation contract for objects that implement 
    /// data retrieval operations against a data source.
    /// </summary>
    public interface IDataProcedure : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>The command text.</value>
        string CommandText { get; set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        DbParameterCollection Parameters { get; }

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>The type of the command.</value>
        CommandType CommandType { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the command text and returns the resulting data in a 
        /// forward-only, as-available data structure.
        /// </summary>
        /// <returns>Returns a <see cref="System.Data.IDataReader" /> object.</returns>
        IDataReader ExecuteDataReader();

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable" />.
        /// </summary>
        /// <returns>Returns a <see cref="System.Data.DataTable" /></returns>
        DataTable ExecuteDataTable();

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataTable" />.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>Returns a <see cref="System.Data.DataTable" /></returns>
        DataTable ExecuteDataTable(CultureInfo culture);

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataSet" />
        /// </summary>
        /// <returns>Returns a <see cref="System.Data.DataSet" /></returns>
        DataSet ExecuteDataSet();

        /// <summary>
        /// Executes the command text and returns the resulting <see cref="System.Data.DataSet" />
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>Returns a <see cref="System.Data.DataSet" /></returns>
        DataSet ExecuteDataSet(CultureInfo culture);

        /// <summary>
        /// Executes the command text and returns the number of rows affected.
        /// </summary>
        int ExecuteNonQuery();

        /// <summary>
        /// Attempts to execute the command text in the form of a scalar function.
        /// </summary>
        /// <returns>Returns the result of the scalar operation.</returns>
        Object ExecuteScalar();

        /// <summary>
        /// Clears the existing command text and parameters.
        /// Does not close any existing connections.
        /// </summary>
        void Clear();

        #endregion
    }
}