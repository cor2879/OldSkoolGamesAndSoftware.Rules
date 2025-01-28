// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelType.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a discrete object which represents a File Type that is supported
    /// by the Rule object Model.
    /// </summary>
    [DataContract(IsReference = true)]   
    public class ModelType 
        : IEquatable<ModelType>
    {
        #region Fields

        /// <summary>
        /// The name
        /// </summary>
        private string name;

        /// <summary>
        /// The object types
        /// </summary>
        private ObjectTypeTable objectTypes;

        /// <summary>
        /// The color type
        /// </summary>
        private Type clrType;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelType"/> class.
        /// </summary>
        public ModelType() 
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ModelType(string name)
            : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The parameter 'name' may not be null.");
            }

            this.Name = name; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="clrType">Type of the color.</param>
        /// <exception cref="System.ArgumentNullException">name;The parameter 'name' may not be null.</exception>
        public ModelType(string name, Type clrType)
            : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The parameter 'name' may not be null.");
            }

            this.Name = name;
            this.ClrType = clrType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelType"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="name">The name.</param>
        /// <param name="clrType">Type of the color.</param>
        /// <param name="objectTypes">The object types.</param>
        /// <exception cref="System.ArgumentNullException">name;The parameter 'name' may not be null.</exception>
        public ModelType(Guid identity, string name, Type clrType, IEnumerable<ObjectType> objectTypes)
            : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "The parameter 'name' may not be null.");
            }

            this.Identity = identity;
            this.Name = name;
            this.ClrType = clrType;

            if (objectTypes != null)
            {
                foreach (var objectType in objectTypes)
                {
                    this.AddObjectType(objectType);
                }
            }
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
        public string Name
        {
            get { return this.name; }
            
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value", "The 'Name' property may not be null or empty.");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the color.
        /// </summary>
        /// <value>
        /// The type of the color.
        /// </value>
        [DataMember]
        public Type ClrType
        {
            get { return this.clrType; }
            set { this.clrType = value; }
        }

        /// <summary>
        /// Gets the object types.
        /// </summary>
        /// <value>The object types.</value>
        [DataMember]
        public ObjectTypeTable ObjectTypes
        {
            get { return this.objectTypes; }
            internal set { this.objectTypes = value; }
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
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ModelType);
        }

        /// <summary>
        /// Determines whether the specified <see cref="OldSkoolGamesAndSoftware.Rules.ModelType" /> is equal to this instance.
        /// </summary>
        /// <param name="other">
        /// The <see cref="OldSkoolGamesAndSoftware.Rules.ModelType" /> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="OldSkoolGamesAndSoftware.Rules.ModelType"/> 
        /// is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ModelType other)
        {
            if (((object)other) == null)
            {
                return false;
            }

            return this.Identity.Equals(other.Identity);
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

        /// <summary>
        /// Adds a new child ObjectType instance to this FileType.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        internal void AddObjectType(ObjectType objectType)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType", "The parameter 'objectType' may not be null.");
            }

            if (!this.Equals(objectType.ModelType))
            {
                objectType.ModelType = this;
            }

            if (objectType.Identity.Equals(Guid.Empty))
            {
                throw new InvalidOperationException("An object type with an invalid Identity may not be added to the table.");
            }
            else if (!this.ObjectTypes.Contains(objectType))
            {
                this.ObjectTypes.Add(objectType);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.objectTypes = new ObjectTypeTable();
        }

        #endregion
    }
}