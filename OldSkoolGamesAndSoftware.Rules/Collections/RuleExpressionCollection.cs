// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleExpressionCollection.cs" company="Old Skool Games and Software">
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
    /// Defines a collection of items which implement the RuleExpressionBase interface.
    /// </summary>
    [CollectionDataContract(IsReference = true)]
    public class RuleExpressionCollection 
        : IList<RuleExpressionBase>
    {
        #region Fields

        /// <summary>
        /// The inner list
        /// </summary>
        private List<RuleExpressionBase> innerList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionCollection"/> class.
        /// </summary>
        public RuleExpressionCollection()
        {
            this.innerList = new List<RuleExpressionBase>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionCollection"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public RuleExpressionCollection(int capacity)
        {
            this.innerList = new List<RuleExpressionBase>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExpressionCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public RuleExpressionCollection(IEnumerable<RuleExpressionBase> collection)
        {
            this.innerList = new List<RuleExpressionBase>();

            foreach (RuleExpressionBase item in collection)
            {
                this.Add(item);
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
            get { return ((ICollection<RuleExpressionBase>)this.innerList).IsReadOnly; }
        }

        /// <summary>
        /// Gets or sets the <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// Returns the <see cref="RuleExpressionBase" /> instance found at the specified zero-based index
        /// </returns>
        /// <exception cref="System.ArgumentNullException">value;Null items are not valid for this collection type.</exception>
        public RuleExpressionBase this[int index]
        {
            get { return this.innerList[index]; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Null items are not valid for this collection type.");
                }

                this.innerList[index] = value;
            }
        }

        #endregion

        #region IList<RuleExpressionBase> Members

        /// <summary>
        /// Returns the zero-based index of the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the zero-based index of the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance, or
        /// -1 if it is not found in the collection.
        /// </returns>
        public int IndexOf(RuleExpressionBase item)
        {
            return this.innerList.IndexOf(item);
        }

        /// <summary>
        /// Inserts the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance into the collection at the
        /// specified zero-based index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        public void Insert(int index, RuleExpressionBase item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "The parameter 'item' may not be null.");
            }

            this.innerList.Insert(index, item);
        }

        /// <summary>
        /// Removes the <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance found at the specified
        /// zero-based index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
        }

        #endregion

        #region ICollection<RuleExpressionBase> Members

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(RuleExpressionBase item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "The parameter 'item' may not be null.");
            }

            this.innerList.Add(item);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.innerList.Clear();
        }

        /// <summary>
        /// Determines whether this instance contains the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if this instance contains the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleExpressionBase" /> instance; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(RuleExpressionBase item)
        {
            return this.innerList.Contains(item);
        }

        /// <summary>
        /// Copies the contents of this instance to an array, starting at the specified zero-based index.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">The zero-based index of the array at which to begin copying.</param>
        public void CopyTo(RuleExpressionBase[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if the specified item is found in the List and successfully removed,
        /// otherwise <c>false</c>
        /// </returns>
        public bool Remove(RuleExpressionBase item)
        {
            return this.innerList.Remove(item);
        }

        #endregion

        #region IEnumerable<RuleExpressionBase> Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerator&lt;RuleExpressionBase&gt;"/> object that 
        /// can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RuleExpressionBase> GetEnumerator()
        {
            return ((IEnumerable<RuleExpressionBase>)this.innerList).GetEnumerator();
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
            return ((IEnumerable)this.innerList).GetEnumerator();
        }

        #endregion
    }
}