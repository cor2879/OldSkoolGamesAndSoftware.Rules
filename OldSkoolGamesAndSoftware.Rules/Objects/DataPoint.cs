// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataPoint.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Defines the base type for data points.
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType("GetKnownTypes")]
    [Serializable]
    public abstract class DataPointBase
    {
        #region Fields

        /// <summary>
        /// The identifier
        /// </summary>
        private long id;

        /// <summary>
        /// The name
        /// </summary>
        private string name;

        /// <summary>
        /// The parent
        /// </summary>
        private ParentDataPoint parent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointBase"/> class.
        /// </summary>
        internal DataPointBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public long Id
        {
            get { return this.id; }
            internal set { this.id = value; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [DataMember]
        public ParentDataPoint Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can have children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can have children; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CanHaveChildren
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the data point.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A new <see cref="DataPointBase" /> instance.
        /// </returns>
        public static DataPointBase CreateDataPoint(string name, IComparable value)
        {
            return new DataPoint { Name = name, Value = value };
        }

        /// <summary>
        /// Creates the data point.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// A new <see cref="ParentDataPoint" /> instance.
        /// </returns>
        public static ParentDataPoint CreateDataPoint(string name)
        {
            return new ParentDataPoint
            {
                Name = name
            };
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The value
        /// </returns>
        public abstract object GetValue();

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public abstract void SetValue(object value);

        /// <summary>
        /// Gets the type of the value color.
        /// </summary>
        /// <returns>
        /// The CLR Type of the value
        /// </returns>
        public abstract Type GetValueClrType();

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>
        /// The DataPoint children
        /// </returns>
        public abstract IEnumerable<DataPointBase> GetChildren();

        #endregion
    }

    /// <summary>
    /// Defines a single data point which represents a key/value pair (one key, one value).
    /// </summary>
    [DataContract(IsReference = true)]
    public class DataPoint
        : DataPointBase
    {
        #region Fields

        /// <summary>
        /// The value
        /// </summary>
        private IComparable value;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [DataMember]
        public IComparable Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can have children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can have children; otherwise, <c>false</c>.
        /// </value>
        public override bool CanHaveChildren
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>
        /// Not Implemented
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// Not Implemented
        /// </exception>
        public override IEnumerable<DataPointBase> GetChildren()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The value.
        /// </returns>
        public override object GetValue()
        {
            return this.Value;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public override void SetValue(object value)
        {
            this.Value = (IComparable)value;
        }

        /// <summary>
        /// Gets the type of the value color.
        /// </summary>
        /// <returns>
        /// The CLR type of the value.
        /// </returns>
        public override Type GetValueClrType()
        {
            return this.Value.GetType();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var formatString = (this.Value is string) ? "{0}: \"{1}\"" : "{0}: {1}";

            return string.Format(formatString, this.Name, this.Value);
        }

        #endregion
    }

    /// <summary>
    /// Defines a single data point which represents a parent of a series of data points.
    /// </summary>
    [DataContract(IsReference = true)]
    public class ParentDataPoint
        : DataPointBase
    {
        #region Fields

        /// <summary>
        /// The children
        /// </summary>
        private DataPointChildren children;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParentDataPoint"/> class.
        /// </summary>
        public ParentDataPoint()
        {
            this.children = new DataPointChildren();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        [DataMember]
        public DataPointChildren Children
        {
            get { return this.children; }
            set { this.children = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can have children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can have children; otherwise, <c>false</c>.
        /// </value>
        public override bool CanHaveChildren
        {
            get { return true; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>
        /// The children
        /// </returns>
        public override IEnumerable<DataPointBase> GetChildren()
        {
            return this.Children;
        }

        /// <summary>
        /// Gets the type of the value color.
        /// </summary>
        /// <returns>
        /// Because this type of Datapoint has children instead of a value, this method
        /// always returns typeof(IEnumerable)
        /// </returns>
        public override Type GetValueClrType()
        {
            return typeof(IEnumerable);
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void AddChild(DataPointBase child)
        {
            child.Parent = this;
            this.children.Items.Add(child);
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The new <see cref="DataPointBase" /> instance
        /// </returns>
        public DataPointBase AddChild(string name, IComparable value)
        {
            var dataPoint = DataPointBase.CreateDataPoint(name, value);
            dataPoint.Parent = this;
            this.Children.Add(dataPoint);

            return dataPoint;
        }

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="dataPoint">The dataPoint.</param>
        /// <returns>
        /// <c>true</c> if the dataPoint is found in the children and successfully removed,
        /// otherwise <c>false</c>
        /// </returns>
        public bool RemoveChild(DataPointBase dataPoint)
        {
            if (dataPoint != null)
            {
                dataPoint.Parent = null;
                return this.Children.Remove(dataPoint);
            }

            return false;
        }

        /// <summary>
        /// Removes the child at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveChildAt(int index)
        {
            this.Children[index].Parent = null;
            this.Children.RemoveAt(index);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(this.Name);
            stringBuilder.Append(": { ");
            
            if (this.Children.Any())
            {
                stringBuilder.Append(this.Children.First());

                foreach (var child in this.Children.Skip(1))
                {
                    stringBuilder.Append(", ");
                    stringBuilder.Append(child);
                }
            }

            stringBuilder.Append('}');

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// Not Implemented
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// Not Implemented
        /// </exception>
        public override object GetValue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.NotImplementedException">
        /// Not Implemented
        /// </exception>
        public override void SetValue(object value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SubClasses

        /// <summary>
        /// Defines a collection which contains all Child <see cref="DataPointBase" /> instances of a
        /// <see cref="ParentDataPoint" /> instance.
        /// </summary>
        [DataContract]
        [Serializable]
        public class DataPointChildren
            : IEnumerable<DataPointBase>
        {
            #region Fields

            /// <summary>
            /// The items
            /// </summary>
            private DataPointCollection items;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="DataPointChildren"/> class.
            /// </summary>
            internal DataPointChildren()
            {
                this.items = new DataPointCollection();
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DataPointChildren"/> class.
            /// </summary>
            /// <param name="children">The children.</param>
            internal DataPointChildren(IEnumerable<DataPointBase> children)
            {
                this.items = new DataPointCollection(children);
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the count.
            /// </summary>
            /// <value>
            /// The count.
            /// </value>
            public int Count
            {
                get { return this.items.Count; }
            }

            /// <summary>
            /// Gets or sets the items.
            /// </summary>
            /// <value>
            /// The items.
            /// </value>
            [DataMember]
            internal DataPointCollection Items
            {
                get { return this.items; }
                set { this.items = value; }
            }

            /// <summary>
            /// Gets the <see cref="DataPointBase"/> at the specified index.
            /// </summary>
            /// <value>
            /// The <see cref="DataPointBase"/>.
            /// </value>
            /// <param name="index">The index.</param>
            /// <returns>
            /// The <see cref="DataPointBase" /> instance found at the specified zero-based index.
            /// </returns>
            public DataPointBase this[int index]
            {
                get { return this.items[index]; }
                internal set { this.items[index] = value; }
            }

            #endregion

            #region IEnumerable<DataPointBase> Members

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<DataPointBase> GetEnumerator()
            {
                return this.items.GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.items.GetEnumerator();
            }

            #endregion
            
            #region Methods

            /// <summary>
            /// Adds the specified dataPoint.
            /// </summary>
            /// <param name="dataPoint">The dataPoint.</param>
            internal void Add(DataPointBase dataPoint)
            {
                this.items.Add(dataPoint);
            }

            /// <summary>
            /// Removes the specified dataPoint.
            /// </summary>
            /// <param name="dataPoint">The dataPoint.</param>
            /// <returns>
            /// <c>true</c> if the specified DataPoint is found,
            /// otherwise <c>false</c>
            /// </returns>
            internal bool Remove(DataPointBase dataPoint)
            {
                return this.items.Remove(dataPoint);
            }

            /// <summary>
            /// Removes at.
            /// </summary>
            /// <param name="index">The index.</param>
            internal void RemoveAt(int index)
            {
                this.items.RemoveAt(index);
            }

            #endregion
        }

        #endregion
    }
}
