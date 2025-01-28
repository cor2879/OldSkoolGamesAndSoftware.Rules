using System;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines an attribute class that assists with custom deconstruction of
    /// an object to Xml via use of the ToXml static method found in the
    /// GroupBConsulting.Lib.Tools class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class XmlExportAttribute : System.Attribute, IComparable<XmlExportAttribute>
    {
        #region Fields

        private bool _isExported = true;
        private bool _ignoreIfNullOrEmpty;
        private string _exportName = String.Empty;
        private bool _printMembersOnly;
        private string _dateTimeFormat = String.Empty;
        private XmlExportChildType _xmlExportChildType = XmlExportChildType.Node;
        private int _exportOrder = -1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlExportAttribute"/> class.
        /// </summary>
        public XmlExportAttribute()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is exported.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is exported; otherwise, <c>false</c>.
        /// </value>
        public bool IsExported
        {
            get { return _isExported; }
            set { _isExported = value; }
        }

        /// <summary>
        /// Gets or sets the name of the export.
        /// </summary>
        /// <value>The name of the export.</value>
        public string ExportName
        {
            get { return _exportName; }
            set { _exportName = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [print members only].
        /// </summary>
        /// <value><c>true</c> if [print members only]; otherwise, <c>false</c>.</value>
        public bool PrintMembersOnly
        {
            get { return _printMembersOnly; }
            set { _printMembersOnly = value; }
        }

        /// <summary>
        /// Gets or sets the date time format.  Only valid on Objects of type DateTime (other types
        /// will be ignored).
        /// </summary>
        /// <value>The date time format.</value>
        public string DateTimeFormat
        {
            get { return _dateTimeFormat; }
            set { _dateTimeFormat = value; }
        }

        /// <summary>
        /// Gets or sets the type of the XML export child.
        /// </summary>
        /// <value>The type of the XML export child.</value>
        public XmlExportChildType XmlExportChildType
        {
            get { return _xmlExportChildType; }
            set { _xmlExportChildType = value; }
        }

        /// <summary>
        /// Gets or sets the order in which the property
        /// is exported.  If not specified or if negative,
        /// this value is ignored.
        /// </summary>
        public int ExportOrder
        {
            get { return _exportOrder; }
            set { _exportOrder = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether [the property is ignored if null or empty].
        /// </summary>
        public bool IgnoreIfNullOrEmpty
        {
            get { return _ignoreIfNullOrEmpty; }
            set { _ignoreIfNullOrEmpty = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the specified XmlExportAttributes
        /// </summary>
        /// <param name="lhs">The left XmlExportAttribute.</param>
        /// <param name="rhs">The right XmlExportAttribute.</param>
        /// <returns>
        /// Returns a signed 32-bit integer that is less than zero if the
        /// left item is less than the right item, zero if they are equivalent
        /// in weight, and greater than zero if the left item is greater than
        /// the right item.
        /// </returns>
        public static int Compare(XmlExportAttribute lhs, XmlExportAttribute rhs)
        {
            return (lhs < rhs) ? -1 : (lhs > rhs) ? 1 : 0;      
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return IsExported.GetHashCode() ^ _exportName.GetHashCode() ^ _printMembersOnly.GetHashCode() ^
                _dateTimeFormat.GetHashCode() ^ _xmlExportChildType.GetHashCode() ^ _exportOrder;
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An <see cref="T:System.Object"/> to compare with this instance or null.</param>
        /// <returns>
        /// true if <paramref name="obj"/> equals the type and value of this instance; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as XmlExportAttribute);
        }

        private bool Equals(XmlExportAttribute attrib)
        {
            if ((object)attrib == null)
                return false;

            return (_xmlExportChildType == attrib._xmlExportChildType && _exportName == attrib._exportName &&
                _isExported == attrib._isExported && _printMembersOnly == attrib._printMembersOnly &&
                _exportOrder == attrib._exportOrder && _dateTimeFormat == attrib._dateTimeFormat);
        }

        #endregion

        #region IComparable<XmlExportAttribute>

        int IComparable<XmlExportAttribute>.CompareTo(XmlExportAttribute other)
        {
            return ((this < other) ? -1 : (this > other) ? 1 : 0);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(XmlExportAttribute attribute1, Object value)
        {
            if ((object)attribute1 == null)
                return (value == null);

            return attribute1.Equals(value);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="attribute2">The attribute2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(XmlExportAttribute attribute1, 
            XmlExportAttribute attribute2)
        {
            if ((object)attribute1 == null)
                return ((object)attribute2 == null);

            return attribute1.Equals(attribute2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(XmlExportAttribute attribute1, Object value)
        {
            if ((object)attribute1 == null)
                return value != null;

            return !(attribute1.Equals(value));
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="attribute2">The attribute2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(XmlExportAttribute attribute1, 
            XmlExportAttribute attribute2)
        {
            if ((object)attribute1 == null)
                return ((object)attribute2 != null);

            return !(attribute1.Equals(attribute2));
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="attribute2">The attribute2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(XmlExportAttribute attribute1, 
            XmlExportAttribute attribute2)
        {
            if (attribute1 == null)
                return true;
            if (attribute2 == null)
                return false;

            if (attribute1.XmlExportChildType == XmlExportChildType.Attribute)
            {
                if (attribute2.XmlExportChildType != XmlExportChildType.Attribute)
                    return true;

                if (attribute1.ExportOrder >= 0)
                    if (attribute2.ExportOrder >= 0)
                        return attribute1.ExportOrder < attribute2.ExportOrder;
                    else
                        return true;

                return false;
            }
            else if (attribute2.XmlExportChildType == XmlExportChildType.Attribute)
            {
                return false;
            }
            else if (attribute1.XmlExportChildType == XmlExportChildType.Comment)
            {
                if (attribute2.XmlExportChildType != XmlExportChildType.Comment)
                    return true;

                if (attribute1.ExportOrder >= 0)
                {
                    if (attribute2.ExportOrder >= 0)
                    {
                        return attribute1.ExportOrder < attribute2.ExportOrder;
                    }
                    else
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (attribute2.XmlExportChildType == XmlExportChildType.Comment)
            {
                return false;
            }
            else if (attribute1.XmlExportChildType == XmlExportChildType.Node)
            {
                if (attribute2.XmlExportChildType != XmlExportChildType.Node)
                    return true;

                if (attribute1.ExportOrder >= 0)
                    if (attribute2.ExportOrder >= 0)
                        return attribute1.ExportOrder < attribute2.ExportOrder;
                    else
                        return true;

                return false;
            }
            else if (attribute2.XmlExportChildType == XmlExportChildType.Node)
                return false;
            else
            {
                if (attribute1.ExportOrder >= 0)
                    if (attribute2.ExportOrder >= 0)
                        return attribute1.ExportOrder < attribute2.ExportOrder;
                    else
                        return true;

                return false;
            }
                    
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="attribute2">The attribute2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(XmlExportAttribute attribute1, 
            XmlExportAttribute attribute2)
        {
            return ((attribute1 < attribute2) || (attribute1 == attribute2));
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="attribute2">The attribute2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(XmlExportAttribute attribute1, 
            XmlExportAttribute attribute2)
        {
            if (attribute1 == null)
                return false;
            if (attribute2 == null)
                return true;

            if (attribute1.XmlExportChildType == XmlExportChildType.Literal)
            {
                if (attribute2.XmlExportChildType != XmlExportChildType.Literal)
                    return true;

                if (attribute1.ExportOrder >= 0)
                    if (attribute2.ExportOrder >= 0)
                        return attribute1.ExportOrder > attribute2.ExportOrder;
                    else
                        return false;

                return false;
            }
            else if (attribute2.XmlExportChildType == XmlExportChildType.Literal)
            {
                return false;
            }
            else if (attribute1.XmlExportChildType == XmlExportChildType.Comment)
            {
                if (attribute2.XmlExportChildType != XmlExportChildType.Comment)
                {
                    return true;
                }

                if (attribute1.ExportOrder >= 0)
                {
                    if (attribute2.ExportOrder >= 0)
                    {
                        return attribute1.ExportOrder > attribute2.ExportOrder;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            else if (attribute2.XmlExportChildType == XmlExportChildType.Comment)
            {
                return false;
            }
            else if (attribute1.XmlExportChildType == XmlExportChildType.Node)
            {
                if (attribute2.XmlExportChildType != XmlExportChildType.Node)
                    return true;

                if (attribute1.ExportOrder >= 0)
                    if (attribute2.ExportOrder >= 0)
                        return attribute1.ExportOrder > attribute2.ExportOrder;
                    else
                        return false;

                return false;
            }
            else if (attribute2.XmlExportChildType == XmlExportChildType.Node)
                return false;
            else
            {
                if (attribute1.ExportOrder >= 0)
                    if (attribute2.ExportOrder >= 0)
                        return attribute1.ExportOrder > attribute2.ExportOrder;
                    else
                        return false;

                return false;
            }
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="attribute1">The attribute1.</param>
        /// <param name="attribute2">The attribute2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(XmlExportAttribute attribute1, 
            XmlExportAttribute attribute2)
        {
            return ((attribute1 > attribute2) || (attribute1 == attribute2));
        }

        #endregion
    }

    /// <summary>
    /// Defines whether a class member should be exported as a child node or an
    /// attribute in XML.  If not specified, Node is the default option.
    /// </summary>
    public enum XmlExportChildType
    {
        /// <summary>
        /// Members marked with this option will be exported as child nodes.  If not
        /// specified, Node is the default option.
        /// </summary>
        Node,

        /// <summary>
        /// Members marked with this option will be exported as attributes of the
        /// object element  May not be selected if the "PrintMembersOnly" flag is
        /// set to true.  Members marked as Attribute must be declared before Members
        /// of other types in the class definition and will be printed in the order
        /// they are declared.
        /// </summary>
        Attribute,

        /// <summary>
        /// Members marked with this option will be exported as string literal values
        /// inside the parent node.
        /// </summary>
        Literal,

        /// <summary>
        /// Members marked with this option will be exported as comments inside the parent
        /// node.
        /// </summary>
        Comment
    };
}