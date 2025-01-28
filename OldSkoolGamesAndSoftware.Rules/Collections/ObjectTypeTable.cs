// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectTypeTable.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a hashed table of AutoProcessor ObjectTypes that also implements the
    /// IEnumerable&lt;ObjectType&gt; interface.
    /// </summary>
    [DataContract(IsReference = true)]
    public sealed class ObjectTypeTable
        : IEnumerable<ObjectType>, IEnumerable
    {
        #region Fields

        /// <summary>
        /// The inner dictionary
        /// </summary>
        private Dictionary<Guid, ObjectType> innerDictionary;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectTypeTable"/> class.
        /// </summary>
        internal ObjectTypeTable()
        {
            this.Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this.innerDictionary.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<Guid, ObjectType>>)this.innerDictionary).IsReadOnly; }
        }
        
        /// <summary>
        /// Gets the keys.
        /// </summary>
        public Dictionary<Guid, ObjectType>.KeyCollection Keys
        {
            get { return this.innerDictionary.Keys; }
        }

        /// <summary>
        /// Gets or sets the inner dictionary.
        /// </summary>
        /// <value>
        /// The inner dictionary.
        /// </value>
        [DataMember]
        internal Dictionary<Guid, ObjectType> InnerDictionary
        {
            get { return this.innerDictionary; }
            set { this.innerDictionary = value; }
        }

        /// <summary>
        /// Gets the <see cref="ObjectType" /> with the specified ObjectType Identity.
        /// </summary>
        /// <value>
        /// The <see cref="ObjectType"/>.
        /// </value>
        /// <param name="objectTypeId">The object type identifier.</param>
        /// <returns>
        /// The <see cref="ObjectType" /> instance corresponding to the specified objectId
        /// </returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// Thrown if no ObjectType is found corresponding to the specified objectTypeId
        /// </exception>
        public ObjectType this[Guid objectTypeId]
        {
            get
            {
                if (!this.innerDictionary.ContainsKey(objectTypeId))
                {
                    throw new KeyNotFoundException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "An ObjectType instance with the Identity {0} was not found in the table.", 
                            objectTypeId));
                }

                return this.innerDictionary[objectTypeId];
            }

            internal set { this.innerDictionary[objectTypeId] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified ObjectType.
        /// </summary>
        /// <param name="objectType">The ObjectType.</param>
        public void Add(ObjectType objectType)
        {
            this.innerDictionary.Add(objectType.Identity, objectType);
        }

        /// <summary>
        /// Determines whether this instance contains the specified ObjectType.
        /// </summary>
        /// <param name="objectType">The ObjectType.</param>
        /// <returns>
        /// <c>true</c> if this instance contains the specified ObjectType; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ObjectType objectType)
        {
            return this.innerDictionary.ContainsKey(objectType.Identity);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.innerDictionary.Clear();
        }

        /// <summary>
        /// Copies the members of this instance to the specified array, beginning at the specified
        /// zero based index.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<Guid, ObjectType>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<Guid, ObjectType>>)this.innerDictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Determines whether this table contains a ObjectType instance with the specified ObjectType Identity.
        /// </summary>
        /// <param name="solutionSourceId">The ObjectType Identity.</param>
        /// <returns>
        /// <c>true</c> if this table contains a ObjectType instance with the specified ObjectType Identity; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsSolutionSourceId(Guid solutionSourceId)
        {
            return this.innerDictionary.ContainsKey(solutionSourceId);
        }

        /// <summary>
        /// Removes the specified ObjectType from this table.
        /// </summary>
        /// <param name="objectType">The ObjectType.</param>
        /// <returns>
        /// <c>true</c> if the specified ObjectType is found in the Dictionary and removed,
        /// otherwise <c>false</c>
        /// </returns>
        public bool Remove(ObjectType objectType)
        {
            return this.innerDictionary.Remove(objectType.Identity);
        }

        /// <summary>
        /// Removes a ObjectType that matches the specified ObjectType Identity from this table.
        /// </summary>
        /// <param name="identity">The ObjectType Identity.</param>
        /// <returns>
        /// <c>true</c> if the table contains an ObjectType matching the specified identity and removes it,
        /// otherwise <c>false</c>.
        /// </returns>
        public bool Remove(Guid identity)
        {
            return this.innerDictionary.Remove(identity);
        }

        /// <summary>
        /// Attempts to get a value from the table that matches the specified ObjectTypeId.
        /// </summary>
        /// <param name="identity">The ObjectType Identity.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if an ObjectType matching the specified identity is successfully retrieved,
        /// otherwise <c>false</c>.
        /// </returns>
        public bool TryGetValue(Guid identity, out ObjectType value)
        {
            return this.innerDictionary.TryGetValue(identity, out value);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable<ObjectType> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<ObjectType> IEnumerable<ObjectType>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.innerDictionary = new Dictionary<Guid, ObjectType>();
        }

        #region Nested Classes

        /// <summary>
        /// Defines an Enumerator that can iterate through a ObjectTypeTable instance.
        /// </summary>
        public class Enumerator
            : IEnumerator<ObjectType>
        {
            #region Fields

            /// <summary>
            /// The inner dictionary
            /// </summary>
            private Dictionary<Guid, ObjectType> innerDictionary;

            /// <summary>
            /// The inner enumerator
            /// </summary>
            private IEnumerator<Guid> innerEnumerator;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> class.
            /// </summary>
            /// <param name="table">The table.</param>
            public Enumerator(ObjectTypeTable table)
            {
                this.innerDictionary = table.innerDictionary;
            }

            /// <summary>
            /// Finalizes an instance of the <see cref="Enumerator" /> class.
            /// </summary>
            ~Enumerator()
            {
                this.Dispose(false);
            }

            #endregion

            #region Properties
            
            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            public ObjectType Current
            {
                get
                {
                    return this.innerDictionary[this.innerEnumerator.Current];
                }
            }
            
            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            object IEnumerator.Current
            {
                get { return this.innerDictionary[this.innerEnumerator.Current]; }
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region IEnumerator Members

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            public bool MoveNext()
            {
                if (this.innerEnumerator == null)
                {
                    this.innerEnumerator = this.innerDictionary.Keys.GetEnumerator();
                }

                return this.innerEnumerator.MoveNext();
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            public void Reset()
            {
                if (this.innerEnumerator != null)
                {
                    this.innerEnumerator.Dispose();
                }

                this.innerEnumerator = null;
            }

            #endregion

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; 
            /// <c>false</c> to release only unmanaged resources.</param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (this.innerEnumerator != null)
                    {
                        this.innerEnumerator.Dispose();
                        this.innerEnumerator = null;
                    }
                }
            }
        }

        #endregion
    }
}