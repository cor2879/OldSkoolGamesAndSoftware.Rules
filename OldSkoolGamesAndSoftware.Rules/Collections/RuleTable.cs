// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleTable.cs" company="Old Skool Games and Software">
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
    /// Defines an IEnumerable collection of <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> items which also extends a
    /// constant time access method for retrieving individual <see cref="OldSkoolGamesAndSoftware.Rules.Rule" />
    /// instances based on their 64-bit integer Identity property.
    /// </summary>
    [DataContract(IsReference = true)]
    [Serializable]
    public class RuleTable 
        : IEnumerable<Rule>
    {
        #region Fields

        /// <summary>
        /// The inner dictionary
        /// </summary>
        private Dictionary<Guid, Rule> innerDictionary;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleTable"/> class.
        /// </summary>
        internal RuleTable()
        {
            this.Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable" />.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable" />.
        /// </returns>
        public int Count
        {
            get { return this.innerDictionary.Count; }
        }

        /// <summary>
        /// Gets or sets the inner dictionary.
        /// </summary>
        /// <value>
        /// The inner dictionary.
        /// </value>
        [DataMember]
        internal Dictionary<Guid, Rule> InnerDictionary
        {
            get { return this.innerDictionary; }
            set { this.innerDictionary = value; }
        }
        
        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the 
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object 
        /// that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        internal ICollection<Guid> Keys
        {
            get { return this.innerDictionary.Keys; }
        }
                
        /// <summary>
        /// Gets the <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> instance with the specified identity.
        /// </summary>
        /// <param name="identity">
        /// The 64-bit integer identity of the <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> instance to retrieve.
        /// </param>
        /// <returns>
        /// The <see cref="OldSkoolGamesAndSoftware.Rules.Rule" /> instance with the specified identity.
        /// </returns>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
        /// The property is retrieved and <paramref name="identity"/> is not found.
        /// </exception>
        public Rule this[Guid identity]
        {
            get
            {
                try
                {
                    return this.innerDictionary[identity];
                }
                catch (KeyNotFoundException ex)
                {
                    throw new KeyNotFoundException(
                        string.Format(
                            CultureInfo.CurrentCulture, 
                            "The key value '{0}' was not found in the dictionary.", 
                            identity), 
                        ex);
                }
            }

            internal set { this.innerDictionary[identity] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
        /// </summary>
        /// <param name="identity">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="identity"/> is null.
        /// </exception>
        public bool Contains(Guid identity)
        {
            return this.innerDictionary.ContainsKey(identity);
        }

        /// <summary>
        /// Adds the specified Rule to the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable"/> instance.
        /// </summary>
        /// <param name="item">The Rule instance to add.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the parameter 'item' is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if the parameter 'item' already exists in the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable"/> instance.
        /// </exception>
        public void Add(Rule item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "The parameter 'item' may not be null.");
            }

            try
            {
                this.innerDictionary.Add(item.Identity, item);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("The specified Rule already exists in the Rule Table.", "item", ex);
            }
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable" />.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.
        /// This method also returns false if <paramref name="identity"/> was not found in the original <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable" />.
        /// </returns>
        public bool Remove(Guid identity)
        {
            return this.innerDictionary.Remove(identity);
        }

        /// <summary>
        /// Removes the specified rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <returns>
        /// <c>true</c> if the specified rule is found in the Dictionary and removed,
        /// otherwise <c>false</c>
        /// </returns>
        public bool Remove(Rule rule)
        {
            return this.innerDictionary.Remove(rule.Identity);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(Guid identity, out Rule value)
        {
            return this.innerDictionary.TryGetValue(identity, out value);
        }

        /// <summary>
        /// Removes all items from the <see cref="OldSkoolGamesAndSoftware.Rules.RuleTable"/>.
        /// </summary>
        public void Clear()
        {
            this.innerDictionary.Clear();
        }

        #endregion

        #region IEnumerable<Rule> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Rule> GetEnumerator()
        {
            return new RuleTable.Enumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.innerDictionary = new Dictionary<Guid, Rule>();
        }

        #region Nested Classes

        /// <summary>
        /// Defines an Enumerator that can iterate through an RuleTable instance.
        /// </summary>
        public class Enumerator
            : IEnumerator<Rule>
        {
            #region Fields

            /// <summary>
            /// The inner dictionary
            /// </summary>
            private Dictionary<Guid, Rule> innerDictionary;

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
            public Enumerator(RuleTable table)
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
            public Rule Current
            {
                get { return this.innerDictionary[this.innerEnumerator.Current]; }
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
