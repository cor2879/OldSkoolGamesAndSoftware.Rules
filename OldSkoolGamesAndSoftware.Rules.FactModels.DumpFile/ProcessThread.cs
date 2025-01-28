// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessThread.cs" company="Old Skool Games and Software">
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
    /// Defines an object which contains information regarding
    /// Thread data as described by WinDE.
    /// </summary>
    public class ProcessThread
        : IFact
    {
        #region Fields

        /// <summary>
        /// The object type identifier
        /// </summary>
        public static readonly Guid ObjectTypeId = Guid.Parse("FC628E31-5D94-4351-BD09-60A5C2011D87");

        /// <summary>
        /// The call stack
        /// </summary>
        private List<StackFrame> callStack = new List<StackFrame>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessThread"/> class.
        /// </summary>
        public ProcessThread()
        { }

        #endregion

        #region IFact Members

        /// <summary>
        /// Gets the registered ObjectType that this instance
        /// represents when interacting with the RACS
        /// Annotation framework.
        /// </summary>
        /// <value>The type of the object.</value>
        public ObjectType ObjectType
        {
            get
            {
                return DumpFileModelAssemblyInfo.ModelTypes[DumpFile.ModelTypeId].ObjectTypes[ObjectTypeId];
            }
        }

        /// <summary>
        /// Gets the unique Identity of the object to be annotated.
        /// </summary>
        long IFact.Identity
        {
            get { return this.Id; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets the File with which this instance is associated.
        /// </summary>
        IFactFile IFact.File
        {
            get { return this.File; }
        }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public DumpFile File
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        /// <value>
        /// The process identifier.
        /// </value>
        public long? ProcessId { get; set; }

        /// <summary>
        /// Gets or sets the unique thread.
        /// </summary>
        /// <value>The unique thread.</value>
        public long? UniqueThreadId { get; set; }

        /// <summary>
        /// Gets or sets the function calls.
        /// </summary>
        /// <value>The function calls.</value>
        public List<StackFrame> CallStack
        {
            get { return this.callStack; }
            set { this.callStack = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is handling an exception.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is handling an exception; otherwise, <c>false</c>.
        /// </value>
        public bool IsHandlingException { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to 
        /// this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to 
        /// this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProcessThread);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; 
        /// otherwise, false.
        /// </returns>
        public bool Equals(ProcessThread other)
        {
            if (((object)other) == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and 
        /// data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion
    }
}