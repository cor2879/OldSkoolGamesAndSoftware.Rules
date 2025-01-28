using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// A sorted collection of elements which implement <see cref="IComparable" />.
    /// </summary>
    /// <typeparam name="TComparable">The type of the collection elements.</typeparam>
    [DataContract(IsReference = true)]
    public class SortedCollection<TComparable>
        : IEnumerable<TComparable>
        where TComparable : IComparable
    {
        #region Fields

        /// <summary>
        /// The inner list
        /// </summary>
        protected List<TComparable> innerList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedCollection{TComparable}"/> class.
        /// </summary>
        public SortedCollection()
        {
            innerList = new List<TComparable>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedCollection{TComparable}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public SortedCollection(int capacity)
        {
            innerList = new List<TComparable>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedCollection{TComparable}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public SortedCollection(IEnumerable<TComparable> collection)
        {
            innerList = new List<TComparable>(collection.Count());

            foreach (TComparable item in collection)
            {
                Add(item);
            }
        }

        #endregion

        #region Properties

        [DataMember()]
        internal List<TComparable> InnerList
        {
            get { return innerList; }
            set { innerList = value; }
        }

        /// <summary>
        /// Gets the <see cref="IComparable"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IComparable"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public TComparable this[int index]
        {
            get { return innerList[index]; }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return innerList.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Add(TComparable item)
        {
            if (innerList.Count == 0)
            {
                innerList.Add(item);
            }

            Type type = typeof(TComparable);

            int lastIndex = Count - 1;

            if (!type.IsValueType && (Object.Equals(item, null)  && ((Count > 0) && (this[0] != null))))
            {
                innerList.Insert(0, item);
            }
            else if (item.CompareTo(this[0]) < 0)
            {
                innerList.Insert(0, item);
            }
            else if (item.CompareTo(this[lastIndex]) > 0)
            {
                innerList.Add(item);
            }
            else
            {
                PartitionAdd(item, 0, Count);
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="index">The index.</param>
        public virtual void Add(TComparable item, out int index)
        {
            if (innerList.Count == 0)
            {
                innerList.Add(item);
            }

            Type type = typeof(TComparable);

            int lastIndex = Count - 1;

            if (!type.IsValueType && (Object.Equals(item, null)  && ((Count > 0) && (this[0] != null))))
            {
                innerList.Insert(0, item);
                index = 0;
            }
            else if (item.CompareTo(this[0]) < 0)
            {
                innerList.Insert(0, item);
                index = 0;
            }
            else if (item.CompareTo(this[lastIndex]) > 0)
            {
                innerList.Add(item);
                index = ++lastIndex;
            }
            else
            {
                index = PartitionAdd(item, 0, Count);
            }
        }

        private int PartitionAdd(TComparable item, int start, int count)
        {
            int mid;

            if (count == 0)
            {
                return -1;
            }
            else if (count == 1)
            {
                mid = start;
            }
            else
            {
                mid = start + (count / 2);
            }

            int comparison = item.CompareTo(this[mid]);

            if (comparison == 0)
            {
                if (!item.Equals(this[mid]))
                {
                    innerList.Insert(++mid, item);
                }

                return mid;
            }
            else if (comparison < 0)
            {
                int comparison2 = item.CompareTo(this[Math.Max(mid - 1, 0)]);

                if (comparison2 > 0)
                {
                    innerList.Insert(mid, item);
                    return mid;
                }
                else
                {
                    return PartitionAdd(item, start, Math.Min((mid + 1) - start, Count - start));
                }
            }
            else // comparison > 0
            {
                int nextIndex = Math.Min(mid + 1, Count - 1);
                int comparison2 = item.CompareTo(this[nextIndex]);

                if (comparison2 < 0)
                {
                    innerList.Insert(nextIndex, item);
                    return nextIndex;
                }
                else
                {
                    return PartitionAdd(item, nextIndex, count - (nextIndex - start));
                }
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            innerList.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Contains(TComparable item)
        {
            return PartitionIndexOf(item, 0, Count) > -1;
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int IndexOf(TComparable item)
        {
            return PartitionIndexOf(item, 0, Count);
        }

        private int PartitionIndexOf(TComparable item, int start, int count)
        {
            int mid;

            if (count == 0)
            {
                return -1;
            }
            else if (count == 1)
            {
                mid = start;
            }
            else
            {
                mid = start + (count / 2);
            }

            int comparison = item.CompareTo(this[mid]);

            if (comparison == 0)
            {
                if (item.Equals(this[mid]))
                {
                    return mid;
                }

                while (++mid != Count && item.CompareTo(this[mid]) == 0) 
                {
                    if (item.Equals(this[mid]))
                    {
                        return mid;
                    }
                }

                return -1;
            }
            else if (comparison < 0)
            {
                return PartitionIndexOf(item, start, Math.Min(mid - start, Count - start));
            }
            else // comparison > 0
            {
                int partitionStart = Math.Min(mid + 1, Count - 1);

                return PartitionIndexOf(item, partitionStart, count - (partitionStart - start));
            }
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(TComparable[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(TComparable item)
        {
            int index = PartitionIndexOf(item, 0, Count);

            if (index > -1)
            {
                innerList.RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            innerList.RemoveAt(index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TComparable> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        #endregion
    }
}
