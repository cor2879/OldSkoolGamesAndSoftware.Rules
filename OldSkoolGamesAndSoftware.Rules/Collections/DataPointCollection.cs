// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataPointCollection.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a collection of DataPoint objects.
    /// </summary>
    [DataContract(IsReference = true)]
    [Serializable]
    public class DataPointCollection
        : IEnumerable<DataPointBase>
    {
        #region Fields

        /// <summary>
        /// The inner list
        /// </summary>
        private List<DataPointBase> innerList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointCollection"/> class.
        /// </summary>
        public DataPointCollection()
        {
            this.innerList = new List<DataPointBase>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointCollection"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public DataPointCollection(int capacity)
        {
            this.innerList = new List<DataPointBase>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public DataPointCollection(IEnumerable<DataPointBase> collection)
        {
            this.innerList = new List<DataPointBase>(collection);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this.innerList.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return ((IList)this.innerList).IsReadOnly; }
        }
        
        /// <summary>
        /// Gets or sets the inner list.
        /// </summary>
        /// <value>
        /// The inner list.
        /// </value>
        [DataMember]
        internal List<DataPointBase> InnerList
        {
            get { return this.innerList; }
            set { this.innerList = value; }
        }

        /// <summary>
        /// Gets the <see cref="DataPointBase" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="DataPointBase"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>
        /// Returns the <see cref="DataPointBase" /> instance found at the specified zero-based index
        /// </returns>
        public DataPointBase this[int index]
        {
            get { return this.innerList[index]; }

            internal set
            {
                this.innerList[index] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the zero based index of the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The zero-based index of the specified <see cref="DataPointBase" /> instance, or -1 if not found.
        /// </returns>
        public int IndexOf(DataPointBase item)
        {
            return this.innerList.IndexOf(item);
        }

        /// <summary>
        /// Removes the item found at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(DataPointBase item)
        {
            this.innerList.Add(item);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether this instance contains the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if this instance contains the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(DataPointBase item)
        {
            return this.innerList.Contains(item);
        }

        /// <summary>
        /// Copies the contents of this collection to the specified array, beginning at
        /// the specified zero based array index.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(DataPointBase[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the specified item from the collection.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="DataPointBase" /> instance is found in the list and successfully removed,
        /// otherwise <c>false</c>.
        /// </returns>
        public bool Remove(DataPointBase item)
        {
            return this.innerList.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<DataPointBase> GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }

        #endregion
    }
}