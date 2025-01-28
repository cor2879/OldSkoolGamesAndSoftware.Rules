// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnnotationSqlDal.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OldSkoolGamesAndSoftware.Rules.Sql
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using OldSkoolGamesAndSoftware.Data;

    /// <summary>
    /// Provides an API for interfacing with Annotation data stored in a
    /// Microsoft SQL Server Database.
    /// </summary>
    public class AnnotationSqlDal 
        : IDisposable
    {
        #region Fields

        /// <summary>
        /// The <see cref="SqlProcedure" /> instance.
        /// </summary>
        private SqlProcedure sqlProc;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnotationSqlDal"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public AnnotationSqlDal(string connectionString)
        {
            this.Initialize(connectionString);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AnnotationSqlDal" /> class.
        /// </summary>
        ~AnnotationSqlDal()
        {
            this.Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString 
        { 
            get 
            { 
                return this.sqlProc.ConnectionString; 
            } 
        }

        /// <summary>
        /// Gets the SQL procedure.
        /// </summary>
        /// <value>The SQL procedure.</value>
        public SqlProcedure SqlProcedure 
        { 
            get 
            { 
                return this.sqlProc; 
            } 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Selects all ude file types.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="System.Data.DataSet"/> containing two <see cref="System.Data.DataTable"/> instances.
        /// The first DataTable should contain the discrete FileType records, and the second the ObjectType records
        /// with the foreign key linking them to the appropriate FileType.
        /// </returns>
        public async Task<DataSet> SelectAllFileTypes()
        {
            return await Task.Run(() =>
            {
                SqlProcedure.Clear();

                SqlProcedure.CommandText = "dbo.sp_SelectAllFileTypes";
                SqlProcedure.CommandType = CommandType.StoredProcedure;

                return SqlProcedure.ExecuteDataSet();
            });
        }

        /// <summary>
        /// Provides an interface into a back end data source for searching File Types.
        /// </summary>
        /// <param name="fileTypeName">Name of the file type.</param>
        /// <param name="matchExact">If set to <c>true</c>, only File Types whose name is an exact match for the specified
        /// fileTypeName parameter will be returned.</param>
        /// <returns>
        /// Returns a <see cref="System.Data.DataSet"/> containing two tables.  The first table contains any
        /// File Types matching the search string.  The second table contains any Object Types associated with
        /// the returned File Types.
        /// </returns>
        public async Task<DataSet> LookupFileType(string fileTypeName, bool matchExact)
        {
            return await Task.Run(() =>
            {
                SqlProcedure.Clear();

                SqlProcedure.CommandText = "dbo.sp_LookupFileType";
                SqlProcedure.CommandType = CommandType.StoredProcedure;

                SqlProcedure.Parameters.AddWithValue("fileTypeName", fileTypeName);
                SqlProcedure.Parameters.AddWithValue("matchExact", matchExact);

                return SqlProcedure.ExecuteDataSet();
            });
        }

        /// <summary>
        /// Provides an interface into a back end data source for searching Object Types.
        /// </summary>
        /// <param name="fileTypeId">The file type id associated with the object type(s) to return.</param>
        /// <param name="objectTypeName">Name of the object types to return.</param>
        /// <returns>
        /// Returns a <see cref="System.Data.DataTable"/> with 0 (if no matching result is found) or 1 (if a match is found)
        /// row(s) of Object Type records
        /// </returns>
        public async Task<DataTable> LookupObjectType(Guid fileTypeId, string objectTypeName)
        {
            return await Task.Run(() =>
            {
                SqlProcedure.Clear();

                SqlProcedure.CommandText = "dbo.sp_LookupObjectType";
                SqlProcedure.CommandType = CommandType.StoredProcedure;

                SqlProcedure.Parameters.AddWithValue("fileTypeId", fileTypeId);
                SqlProcedure.Parameters.AddWithValue("name", objectTypeName);

                return SqlProcedure.ExecuteDataTable();
            });
        }

        /// <summary>
        /// Selects the annotations.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// An Asynchronous task, the result of which is a DataSet containing Annotation data obtained from the data source.
        /// </returns>
        public async Task<DataSet> SelectAnnotations(AnnotationSearchParameters parameters)
        {
            return await Task.Run(() =>
            {
                SqlProcedure.Clear();

                SqlProcedure.CommandText = "dbo.sp_LookupAnnotationRoot";
                SqlProcedure.CommandType = CommandType.StoredProcedure;

                SqlProcedure.Parameters.AddWithValue("annotationType", DBNull.Value);

                if (!parameters.FileType.Equals(Guid.Empty))
                {
                    SqlProcedure.Parameters.AddWithValue("fileType", parameters.FileType);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("fileType", DBNull.Value);
                }

                if (!parameters.ObjectType.Equals(Guid.Empty))
                {
                    SqlProcedure.Parameters.AddWithValue("objectType", parameters.ObjectType);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("objectType", DBNull.Value);
                }

                if (parameters.StartDate != null && parameters.StartDate.Value != default(DateTimeOffset))
                {
                    SqlProcedure.Parameters.AddWithValue("startDateCreated", parameters.StartDate.Value);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("startDateCreated", DBNull.Value);
                }

                if (parameters.EndDate != null && parameters.EndDate.Value != default(DateTimeOffset))
                {
                    SqlProcedure.Parameters.AddWithValue("endDateCreated", parameters.EndDate.Value);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("endDateCreated", DBNull.Value);
                }

                SqlProcedure.Parameters.AddWithValue("startDateLastAligned", DBNull.Value);
                SqlProcedure.Parameters.AddWithValue("endDateLastAligned", DBNull.Value);

                string username = string.Format("{0}{1}", string.IsNullOrEmpty(parameters.DomainName) ? string.Empty : string.Format("{0}{1}", parameters.DomainName, '\\'), parameters.SamAccountName);

                if (!string.IsNullOrEmpty(username))
                {
                    SqlProcedure.Parameters.AddWithValue("userName", username);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("userName", DBNull.Value);
                }

                if (parameters.Identity != null)
                {
                    SqlProcedure.Parameters.AddWithValue("identity", parameters.Identity.Value);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("identity", DBNull.Value);
                }

                if (parameters.SolutionSourceId != null)
                {
                    SqlProcedure.Parameters.AddWithValue("solutionSourceId", parameters.SolutionSourceId.Value);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("solutionSourceId", DBNull.Value);
                }

                if (parameters.Inactive != null)
                {
                    SqlProcedure.Parameters.AddWithValue("inactive", parameters.Inactive.Value);
                }
                else
                {
                    SqlProcedure.Parameters.AddWithValue("inactive", false);
                }

                return SqlProcedure.ExecuteDataSet();
            });
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.sqlProc != null)
                {
                    this.sqlProc.Dispose();
                    this.sqlProc = null;
                }
            }
        }

        /// <summary>
        /// Initializes the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private void Initialize(string connectionString)
        {
            this.sqlProc = new SqlProcedure(connectionString);
        }

        #endregion
    }
}
