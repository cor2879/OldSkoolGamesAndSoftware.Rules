// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackFrame.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.FactModels.Dump
{
    using System;

    using OldSkoolGamesAndSoftware.Rules;
    using bc = OldSkoolGamesAndSoftware.Utilities.BinaryConverter;

    /// <summary>
    /// Defines an object which contains information regarding
    /// Function Call data as described by WinDE.
    /// </summary>
    public class StackFrame 
        : IFact
    {
        #region Fields

        /// <summary>
        /// The object type identifier
        /// </summary>
        public static readonly Guid ObjectTypeId = Guid.Parse("E3134689-8FD5-4B4B-AEC2-AA6383E4B0D8");

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
        /// Gets or sets the thread.
        /// </summary>
        public ProcessThread Thread
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the frame.
        /// </summary>
        /// <value>The frame.</value>
        public int Frame { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        public string FunctionName { get; set; }

        /// <summary>
        /// Gets or sets the function offset.
        /// </summary>
        /// <value>The function offset.</value>
        public int FunctionOffset { get; set; }

        /// <summary>
        /// Gets or sets the param1.
        /// </summary>
        /// <value>The param1.</value>
        public long Param1 { get; set; }

        /// <summary>
        /// Gets or sets the param2.
        /// </summary>
        /// <value>The param2.</value>
        public long Param2 { get; set; }

        /// <summary>
        /// Gets or sets the param3.
        /// </summary>
        /// <value>The param3.</value>
        public long Param3 { get; set; }

        /// <summary>
        /// Gets or sets the param4.
        /// </summary>
        /// <value>The param4.</value>
        public long Param4 { get; set; }

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
            return this.Equals(obj as StackFrame);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the 
        /// same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; 
        /// otherwise, false.
        /// </returns>
        public bool Equals(StackFrame other)
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
        /// A hash code for this instance, suitable for use in hashing algorithms 
        /// and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion
    }
}