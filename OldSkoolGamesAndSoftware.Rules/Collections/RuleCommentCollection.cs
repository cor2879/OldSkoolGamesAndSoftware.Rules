// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleCommentCollection.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a collection of RuleComment objects.
    /// </summary>
    [CollectionDataContract(IsReference = true)]
    public class RuleCommentCollection 
        : IEnumerable<RuleComment>
    {
        #region Fields

        /// <summary>
        /// The inner list
        /// </summary>
        private List<RuleComment> innerList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCommentCollection"/> class.
        /// </summary>
        internal RuleCommentCollection()
        {
            this.innerList = new List<RuleComment>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCommentCollection"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        internal RuleCommentCollection(int capacity)
        {
            this.innerList = new List<RuleComment>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCommentCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        internal RuleCommentCollection(IEnumerable<RuleComment> collection)
        {
            this.innerList = new List<RuleComment>(collection);
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
        /// Gets or sets the <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="RuleComment"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>
        /// The <see cref="RuleComment" /> instance found at the specified zero-based index.
        /// </returns>
        public RuleComment this[int index]
        {
            get { return this.innerList[index]; }
            internal set { this.innerList[index] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the zero-based index of the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" />.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The zero-based index of the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" />, or -1 if it does not
        /// exist in the collection.
        /// </returns>
        public int IndexOf(RuleComment item)
        {
            return this.innerList.IndexOf(item);
        }

        /// <summary>
        /// Determines whether this collection contains the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" />.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// <c>true</c> if this collection contains the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" />; 
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(RuleComment item)
        {
            return this.innerList.Contains(item);
        }

        /// <summary>
        /// Copies the elements of this collection to the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array at which to begin copying.</param>
        public void CopyTo(RuleComment[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        #region IEnumerable<RuleComment> Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerator&lt;OldSkoolGamesAndSoftware.Rules.RuleComment&gt;" /> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RuleComment> GetEnumerator()
        {
            return ((IEnumerable<RuleComment>)this.innerList).GetEnumerator();
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

        /// <summary>
        /// Adds the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" />.
        /// </summary>
        /// <param name="item">The item.</param>
        internal void Add(RuleComment item)
        {
            this.innerList.Add(item);
        }

        /// <summary>
        /// Removes the specified <see cref="OldSkoolGamesAndSoftware.Rules.RuleComment" />.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///    <c>true</c> if the item is successfully removed from the collection; <c>false</c> if the item was
        ///     not found in the collection (and therefore could not be removed).
        /// </returns>
        internal bool Remove(RuleComment item)
        {
            return this.innerList.Remove(item);
        }

        #endregion
    }
}