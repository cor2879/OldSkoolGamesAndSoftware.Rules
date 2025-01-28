// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DumpInfo.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.FactModels.Dump
{
    using System;

    using OldSkoolGamesAndSoftware.Rules;
    using bc = OldSkoolGamesAndSoftware.Utilities.BinaryConverter;

    /// <summary>
    /// Defines an object with contains information about
    /// a memory dump, as described by WinDE.
    /// </summary>
    public sealed class DumpInfo 
        : IFact
    {
        #region Fields

        /// <summary>
        /// The object type identifier
        /// </summary>
        public static readonly Guid ObjectTypeId = Guid.Parse("355B1E71-5977-4276-BC5A-5700357A0ABC");

        #endregion
    
        #region IFact Members

        /// <summary>
        /// Gets the unique Identity of the object to be annotated.
        /// </summary>
        long IFact.Identity
        {
            get { return this.Id; }
        }

        /// <summary>
        /// Gets the registered ObjectType that this instance
        /// represents when interacting with the RACS 
        /// Annotation framework.
        /// </summary>
        /// <value>The type of the object.</value>
        public ObjectType ObjectType
        {
            get { return DumpFileModelAssemblyInfo.ModelTypes[DumpFile.ModelTypeId].ObjectTypes[ObjectTypeId]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets the File with which this instance is associated.
        /// </summary>
        IFactFile IFact.File { get { return this.File; } }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public DumpFile File { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the Q word.
        /// </summary>
        /// <value>The Q word.</value>
        public long QWord { get; set; }

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        /// <value>The string value.</value>
        public string StringValue { get; set; }

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
            return this.Equals(obj as DumpInfo);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; 
        /// otherwise, false.
        /// </returns>
        public bool Equals(DumpInfo other)
        {
            if (((object)other) == null)
            {
                return false;
            }

            return (this.PropertyName ?? string.Empty).Equals(other.PropertyName ?? string.Empty);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data 
        /// structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (this.PropertyName ?? string.Empty).GetHashCode();
        }

        #endregion
    }
}