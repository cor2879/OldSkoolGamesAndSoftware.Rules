// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectType.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a singular type of object, which is a child of a FileType,
    /// that may be annotated by an RuleExpression.
    /// </summary>
    [DataContract(IsReference = true)]
    public class ObjectType
        : IEquatable<ObjectType>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectType"/> class.
        /// </summary>
        public ObjectType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectType"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="name">The name.</param>
        public ObjectType(Guid identity, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The parameter 'name' may not be null.");
            }

            this.Identity = identity;
            this.Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>The identity.</value>
        [DataMember]
        public Guid Identity { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        [DataMember]
        public ModelType ModelType { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ObjectType);
        }

        /// <summary>
        /// Determines whether the specified <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="OldSkoolGamesAndSoftware.Rules.ObjectType"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ObjectType other)
        {
            if (((object)other) == null)
            {
                return false;
            }

            return this.GetType().Equals(other.GetType()) && this.Identity.Equals(other.Identity);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Identity.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}