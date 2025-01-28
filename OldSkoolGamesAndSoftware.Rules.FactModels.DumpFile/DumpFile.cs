// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DumpFile.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.FactModels.Dump
{
    using System;
    using System.Collections.Generic;

    using OldSkoolGamesAndSoftware.Rules;
    using bc = OldSkoolGamesAndSoftware.Utilities.BinaryConverter;

    /// <summary>
    /// Defines an object that contains all of the information regarding a single
    /// WinDE debug session.
    /// </summary>
    public class DumpFile 
        : IFactFile, IFact
    {
        #region Fields

        /// <summary>
        /// The model type identifier
        /// </summary>
        public static readonly Guid ModelTypeId = Guid.Parse("D24BA939-B492-40CC-91E7-7FEA8904FD02");

        /// <summary>
        /// The object type identifier
        /// </summary>
        public static readonly Guid ObjectTypeId = Guid.Parse("3BE6C2A8-C0F8-4AB5-9097-CDC33920FBBC");

        /// <summary>
        /// The collection of <see cref="DumpInfo" /> instances associated with this dump.
        /// </summary>
        private List<DumpInfo> dumpInfo = new List<DumpInfo>();

        /// <summary>
        /// The collection of <see cref="ProcessThread" /> instance associated with this dump.
        /// </summary>
        private List<ProcessThread> threads = new List<ProcessThread>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DumpFile"/> class.
        /// </summary>
        public DumpFile()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Model Type that this instance represents.
        /// </summary>
        /// <value>The type of the model.</value>
        public ModelType ModelType
        {
            get { return DumpFileModelAssemblyInfo.ModelTypes[ModelTypeId]; }
        }

        /// <summary>
        /// Gets the <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType" /> of the
        /// Object instance that is being annotated.
        /// </summary>
        public ObjectType ObjectType
        {
            get { return DumpFileModelAssemblyInfo.ModelTypes[ModelTypeId].ObjectTypes[ObjectTypeId]; }
        }

        /// <summary>
        /// Gets or sets the dump info.
        /// </summary>
        /// <value>The dump info.</value>
        public List<DumpInfo> DumpInfo
        {
            get { return this.dumpInfo; }
            set { this.dumpInfo = value; }
        }

        /// <summary>
        /// Gets or sets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public List<ProcessThread> Threads
        {
            get { return this.threads; }
            set { this.threads = value; }
        }
     
        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        /// <value>The request ID.</value>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Gets or sets the file unique identifier.
        /// </summary>
        /// <value>
        /// The file unique identifier.
        /// </value>
        public Guid FileGuid { get; set; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the unique Identity of the object to be annotated.
        /// </summary>
        public long Identity 
        {
            get; set;
        }

        /// <summary>
        /// Gets the File with which this instance is associated.
        /// </summary>
        public IFactFile File
        {
            get { return this; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as DumpFile);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; 
        /// otherwise, false.
        /// </returns>
        public bool Equals(DumpFile other)
        {
            if (((object)other) == null)
            {
                return false;
            }

            return this.RequestId.Equals(other.RequestId);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, sui> for use in hashing algorithms and 
        /// data structures like a hash >. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.RequestId.GetHashCode();
        }

        #endregion
    }
}
