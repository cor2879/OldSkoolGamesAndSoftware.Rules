// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelTypeTable.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.Serialization;

    using OldSkoolGamesAndSoftware.Utilities;

    /// <summary>
    /// Defines a table of <see cref="OldSkoolGamesAndSoftware.Rules.ModelType"/> objects, hashed by their unique 64-bit Identity
    /// </summary>
    [DataContract(IsReference = true)]
    public sealed class ModelTypeTable 
        : IDictionary<Guid, ModelType>, IEnumerable<ModelType>
    {
        #region Fields

        /// <summary>
        /// The inner dictionary
        /// </summary>
        private Dictionary<Guid, ModelType> innerDictionary;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelTypeTable"/> class.
        /// </summary>
        public ModelTypeTable()
        {
            this.innerDictionary = new Dictionary<Guid, ModelType>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelTypeTable"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ModelTypeTable(int capacity)
        {
            this.innerDictionary = new Dictionary<Guid, ModelType>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelTypeTable"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ModelTypeTable(IEnumerable<ModelType> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "The parameter 'collection' may not be null.");
            }

            this.innerDictionary = new Dictionary<Guid, ModelType>(collection.Count());

            foreach (var item in collection)
            {
                this.TryAdd(item);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelTypeTable"/> class.
        /// </summary>
        /// <param name="dataSet">The <see cref="System.Data.DataSet" /> instance from which to build the FileTypeTable.</param>
        public ModelTypeTable(DataSet dataSet)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet", "The parameter 'dataSet' may not be null.");
            }

            var fileTypeTable = dataSet.Tables[0];
            var objectTypeTable = dataSet.Tables[1];

            this.innerDictionary = new Dictionary<Guid, ModelType>(fileTypeTable.Rows.Count);

            foreach (DataRow row in fileTypeTable.Rows)
            {
                var ft = new ModelType();

                ft.Identity = Tools.Convert(row["Identity"], default(Guid));
                ft.Name = row["Name"].ToString();

                this.Add(ft);
            }

            if (objectTypeTable != null)
            {
                foreach (DataRow row in objectTypeTable.Rows)
                {
                    var objectType = new ObjectType();

                    objectType.Identity = Tools.Convert(row["Identity"], default(Guid));
                    objectType.Name = row["Name"].ToString();

                    var fileType = this[Tools.Convert(row["FileTypeId"], default(Guid))];

                    objectType.ModelType = fileType;
                    fileType.AddObjectType(objectType);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
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
            get { return ((ICollection<KeyValuePair<Guid, ModelType>>)this.innerDictionary).IsReadOnly; }
        }

        #endregion

        #region IDictionary<int, FileType> Members

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public ICollection<Guid> Keys
        {
            get { return this.innerDictionary.Keys; }
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>The values.</value>
        public ICollection<ModelType> Values
        {
            get { return this.innerDictionary.Values; }
        }

        /// <summary>
        /// Gets or sets the inner dictionary.
        /// </summary>
        /// <value>
        /// The inner dictionary.
        /// </value>
        [DataMember]
        internal Dictionary<Guid, ModelType> InnerDictionary
        {
            get { return this.innerDictionary; }
            set { this.innerDictionary = value; }
        }

        /// <summary>
        /// Gets the <see cref="OldSkoolGamesAndSoftware.Rules.ModelType" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// Returns the <see cref="ModelType" /> instance found at the specified zero-based index
        /// </returns>
        public ModelType this[Guid key] { get { return this.innerDictionary[key]; } }

        /// <summary>
        /// Gets or sets the <see cref="OldSkoolGamesAndSoftware.Rules.ModelType" /> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// Returns the <see cref="ModelType" /> instance found at the specified zero-based index
        /// </returns>
        ModelType IDictionary<Guid, ModelType>.this[Guid key]
        {
            get { return this.innerDictionary[key]; }
            set { this.innerDictionary[key] = value; }
        }

        #endregion

        #region ICollection<KeyValuePair<Guid,FileType>> Members

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(KeyValuePair<Guid, ModelType> item)
        {
            ((ICollection<KeyValuePair<Guid, ModelType>>)this.innerDictionary).Add(item);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.innerDictionary.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(KeyValuePair<Guid, ModelType> item)
        {
            return ((ICollection<KeyValuePair<Guid, ModelType>>)this.innerDictionary).Contains(item);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<Guid, ModelType>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<Guid, ModelType>>)this.innerDictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if the specified pair exists and is successfully removed,
        /// otherwise <c>false</c>.
        /// </returns>
        public bool Remove(KeyValuePair<Guid, ModelType> item)
        {
            return ((ICollection<KeyValuePair<Guid, ModelType>>)this.innerDictionary).Remove(item);
        }

        /// <summary>
        /// Gets the object types.
        /// </summary>
        /// <returns>
        /// Returns the ObjectTypes associated with this ModelType.
        /// </returns>
        public ObjectTypeTable GetObjectTypes()
        {
            var objectTypes = new ObjectTypeTable();

            foreach (var fileType in this)
            {
                foreach (var objectType in fileType.ObjectTypes)
                {
                    objectTypes.Add(objectType);
                }
            }

            return objectTypes;
        }

        #endregion
        
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <c>true</c> if the specified key is found in the Dictionary and its value removed,
        /// otherwise <c>false</c>
        /// </returns>
        public bool Remove(Guid key)
        {
            return this.innerDictionary.Remove(key);
        }

        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the value is obtained successfully,
        /// otherwise <c>false</c>
        /// </returns>
        public bool TryGetValue(Guid key, out ModelType value)
        {
            return this.innerDictionary.TryGetValue(key, out value);
        }
        
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void IDictionary<Guid, ModelType>.Add(Guid key, ModelType value)
        {
            this.innerDictionary.Add(key, value);
        }

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(Guid key)
        {
            return this.innerDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(ModelType value)
        {
            this.innerDictionary.Add(value.Identity, value);
        }

        /// <summary>
        /// Attempts to add the specified FileType value to the table and will not
        /// throw an exception if adding fails..
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the value is successfully added, otherwise <c>false</c>
        /// </returns>
        public bool TryAdd(ModelType value)
        {
            if (this.innerDictionary.ContainsKey(value.Identity))
            {
                return false;
            }

            this.innerDictionary.Add(value.Identity, value);

            return true;
        }

        /// <summary>
        /// Determines whether the table contains the specified <see cref="OldSkoolGamesAndSoftware.Rules.ModelType" /> object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the table contains the specified; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ModelType value)
        {
            return this.innerDictionary.ContainsKey(value.Identity);
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the value is found in the Dictionary and successfully removed,
        /// otherwise <c>false</c>
        /// </returns>
        public bool Remove(ModelType value)
        {
            return this.innerDictionary.Remove(value.Identity);
        }

        #region IEnumerable<FileType> Members

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// An Enumerator for this Table.
        /// </returns>
        public IEnumerator<ModelType> GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable<KeyValuePair<Guid,FileType>> Members

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// An Enumerator for this Table.
        /// </returns>
        IEnumerator<KeyValuePair<Guid, ModelType>> IEnumerable<KeyValuePair<Guid, ModelType>>.GetEnumerator()
        {
            return ((ICollection<KeyValuePair<Guid, ModelType>>)this.innerDictionary).GetEnumerator();
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
            return ((IEnumerable)this.innerDictionary).GetEnumerator();
        }

        #endregion

        #region Sub Classes

        /// <summary>
        /// Provides Enumeration for instances of this type.
        /// </summary>
        internal class Enumerator 
            : IEnumerator<ModelType>
        {
            #region Fields

            /// <summary>
            /// The initial count
            /// </summary>
            private int initialCount;

            /// <summary>
            /// The inner table
            /// </summary>
            private ModelTypeTable innerTable;

            /// <summary>
            /// The keys
            /// </summary>
            private Guid[] keys;

            /// <summary>
            /// The current index
            /// </summary>
            private int currentIndex;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> class.
            /// </summary>
            /// <param name="table">The table.</param>
            internal Enumerator(ModelTypeTable table)
            {
                this.innerTable = table;
                this.keys = new Guid[this.innerTable.Count];
                this.innerTable.Keys.CopyTo(this.keys, 0);
                this.initialCount = this.innerTable.Count;

                this.Reset();
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
            /// Gets the current.
            /// </summary>
            /// <value>The current.</value>
            public ModelType Current
            {
                get { return this.innerTable[this.keys[this.currentIndex]]; }
            }

            /// <summary>
            /// Gets the current.
            /// </summary>
            /// <value>The current.</value>
            object IEnumerator.Current
            {
                get { return this.innerTable[this.keys[this.currentIndex]]; }
            }

            #endregion

            #region IEnumerator<FileType> Members

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
            /// true if the enumerator was successfully advanced to the next element; 
            /// false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">
            /// The collection was modified after the enumerator was created. 
            /// </exception>
            public bool MoveNext()
            {
                if (!this.initialCount.Equals(this.innerTable.Count))
                {
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");
                }

                return ++this.currentIndex < this.innerTable.Count;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            public void Reset()
            {
                if (!this.initialCount.Equals(this.innerTable.Count))
                {
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");
                }

                this.currentIndex = -1;
            }

            #endregion

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
                    this.innerTable = null;
                    this.keys = null;
                }
            }
        }

        #endregion
    }
}