using System;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines a class level Attribute that allows developers to specify class
    /// level behavior for Xml Export.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class XmlClassDefAttribute : System.Attribute
    {
        #region Fields

        private XmlClassDefOrdering _ordering = XmlClassDefOrdering.Default;

        #endregion

        #region Properties

        /// <summary>
        /// Defines whether instances of the class should be
        /// exported in the default ordering, or in user
        /// defined order.  The XmlExportAttribute property
        /// "ExportOrder" is only used if this property is
        /// set to "UserDefined".
        /// </summary>
        public XmlClassDefOrdering Ordering
        {
            get { return _ordering; }
            set { _ordering = value; }
        }

        #endregion
    }

    /// <summary>
    /// Defines the possible ClassDefOrdering options for the XmlClassDefAttribute class.
    /// </summary>
    public enum XmlClassDefOrdering
    {
        /// <summary>
        /// The class will be exported to xml using no special ordering -items are
        /// exported in the order they appear in code.
        /// </summary>
        Default = 0,

        /// <summary>
        /// The class will be exported in the order specified by the developer.  This
        /// option should be used for classes in an inheritance hierarchy other than
        /// System.Object.
        /// </summary>
        UserDefined
    };
}