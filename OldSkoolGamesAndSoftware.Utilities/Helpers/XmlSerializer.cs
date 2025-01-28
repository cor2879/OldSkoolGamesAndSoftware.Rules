using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Provides functionality for Serializing .Net objects to a generic XML format
    /// corresponding to the object and its properties.
    /// </summary>
    public class XmlSerializer
    {
        #region Fields

        private Dictionary<Object, Int32> _referenceTable;
        private static readonly Type runtimeType = Type.GetType("System.RuntimeType");

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializer"/> class.
        /// </summary>
        public XmlSerializer()
        {
            Initialize();
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            _referenceTable = new Dictionary<object, int>();
        }

        #region ToXml<T>

        /// <summary>
        /// Defines the entry point for the ToXml method.  Exports the data found
        /// in the collection of objects to the specified stream object.
        /// </summary>
        /// <typeparam name="T">
        /// The System.Type of the objects contained in the collection to export.
        /// </typeparam>
        /// <param name="collection">
        /// The collection of objects to export.  May not be null.
        /// </param>
        /// <param name="stream">
        /// The System.IO.TextWriter stream object to which the Xml data will be
        /// exported.
        /// </param>
        public void ToXml<T>(IEnumerable<T> collection, TextWriter stream)
        {
            ToXml<T>(collection, stream, null);
        }

        /// <summary>
        /// Defines the entry point for the ToXml method.  Exports the data found
        /// in the collection of objects to the specified stream object.
        /// </summary>
        /// <typeparam name="T">
        /// The System.Type of the objects contained in the collection to export.
        /// </typeparam>
        /// <param name="collection">
        /// The collection of objects to export.  May not be null.
        /// </param>
        /// <param name="stream">
        /// The System.IO.TextWriter stream object to which the Xml data will be
        /// exported.
        /// </param>
        /// <param name="headerComment">
        /// Defines data to be parsed in a comments section at the top of the XML document.
        /// Pass null if no comments section is desired.
        /// </param>
        public void ToXml<T>(IEnumerable<T> collection, TextWriter stream, XmlComment headerComment)
        {
            Initialize();

            if (collection == null)
                throw new ArgumentNullException("collection");

            using (XmlTextWriter xmlWriter = new XmlTextWriter(stream))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 5;

                xmlWriter.WriteStartDocument();

                if (headerComment != null)
                {
                    xmlWriter.WriteWhitespace(Environment.NewLine);
                    xmlWriter.WriteString(headerComment.GetInnerXml());
                    xmlWriter.WriteWhitespace(Environment.NewLine);
                }

                xmlWriter.WriteStartElement(collection.GetType().Name);

                // If the collection is empty, don't write any data, otherwise start the
                // recursive loop to write the xml
                foreach (T obj in collection)
                {
                    ToXmlFragment<T>(obj, xmlWriter);
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();
            }
        }

        /// <summary>
        /// Defines the entry point for the ToXml method.  Exports the data found
        /// in the specified object of type T to the specified stream object.
        /// </summary>
        /// <typeparam name="T">
        /// The System.Type of the object to export.
        /// </typeparam>
        /// <param name="obj">
        /// The object to export.  May not be null.
        /// </param>
        /// <param name="stream">
        /// The System.IO.TextWriter stream object to which the Xml data will be
        /// exported.
        /// </param>
        public void ToXml<T>(T obj, TextWriter stream)
        {
            ToXml<T>(obj, stream, null);
        }

        /// <summary>
        /// Defines the entry point for the ToXml method.  Exports the data found
        /// in the specified object of type T to the specified stream object.
        /// </summary>
        /// <typeparam name="T">
        /// The System.Type of the object to export.
        /// </typeparam>
        /// <param name="obj">
        /// The object to export.  May not be null.
        /// </param>
        /// <param name="stream">
        /// The System.IO.TextWriter stream object to which the Xml data will be
        /// exported.
        /// </param>
        /// <param name="headerComment">
        /// Defines data to be parsed in a comments section at the top of the XML document.
        /// Pass null if no comments section is desired.
        /// </param>
        public void ToXml<T>(T obj, TextWriter stream, XmlComment headerComment)
        {
            Initialize();

            using (XmlTextWriter xmlWriter = new XmlTextWriter(stream))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 5;

                xmlWriter.WriteStartDocument();

                if (headerComment != null)
                {
                    xmlWriter.WriteWhitespace(Environment.NewLine);
                    xmlWriter.WriteComment(headerComment.GetInnerXml());
                    xmlWriter.WriteWhitespace(Environment.NewLine);
                }

                ToXmlFragment<T>(obj, xmlWriter);

                xmlWriter.WriteEndDocument();
            }
        }

        /// <summary>
        /// Exports the data found in the specified object of type T to the specified stream object
        /// As an XML fragment - this means the XML returned will not contain opening/closing document
        /// tags required by W3 standards for a valid XML document.
        /// </summary>
        /// <typeparam name="T">
        /// The System.Type of the object to export.
        /// </typeparam>
        /// <param name="obj">
        /// The object to export.  May not be null.
        /// </param>
        /// <param name="xmlWriter">
        /// The System.Xml.XmlTextWriter stream object to which the Xml data will be
        /// exported.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the parameter 'xmlWriter' is null.
        /// </exception>"
        public void ToXmlFragment<T>(T obj, XmlTextWriter xmlWriter)
        {
            if (xmlWriter == null)
            {
                throw new ArgumentNullException("xmlWriter", "The parameter 'xmlWriter' may not be null.");
            }

            if (!(obj is ValueType) && ((object)obj) == null)
            {
                return;
            }

            if (!IsComplexType(typeof(T)))
            {
                xmlWriter.WriteString(obj.ToString());
            }
            else
            {
                xmlWriter.WriteStartElement(obj.GetType().Name);

                if (!_referenceTable.ContainsKey(obj))
                {
                    int id = _referenceTable.Count;

                    _referenceTable.Add(obj, id);

                    xmlWriter.WriteAttributeString("id", id.ToString(CultureInfo.CurrentCulture));

                    IEnumerable collection = obj as IEnumerable;

                    if (collection != null)
                    {
                        // If the collection is empty, don't write any data, otherwise start the
                        // recursive loop to write the xml
                        foreach (object element in collection)
                        {
                            if (element == null)
                                continue;

                            // Write an opening element for each member of the collection.
                            xmlWriter.WriteStartElement(element.GetType().Name);

                            ToXml(element, xmlWriter);

                            xmlWriter.WriteEndElement();
                        }
                    }
                    else
                    {
                        ToXml<T>(obj, xmlWriter);
                    }
                }
                else
                {
                    xmlWriter.WriteAttributeString("refid", _referenceTable[obj].ToString(CultureInfo.CurrentCulture));
                }


                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Exports the data found in an object of type T to an xml stream.
        /// Assumes that the Xml stream already exists and has been opened,
        /// and that the opening and closing tags for this object are handled
        /// outside of this method.
        /// </summary>
        /// <typeparam name="T">
        /// The System.Type of the object to export.
        /// </typeparam>
        /// <param name="obj">
        /// The object to export.
        /// </param>
        /// <param name="xmlWriter">
        /// The Xml Stream to which the data will be exported.
        /// </param>
        private void ToXml<T>(T obj, XmlTextWriter xmlWriter)
        {
            Type type = ((obj is ValueType || ((object)obj) != null) ?
                obj.GetType() :
                typeof(T));

            XmlClassDefAttribute[] classAttribs;

            IEnumerable collection = obj as IEnumerable;

            if (collection != null && Tools.CountIEnumerable(collection) < 1)
            {
                return;
            }
            else
            {
                // Get the collection of properties associated with T.
                PropertyInfo[] propertiesInfo = type.GetProperties();
                classAttribs = (XmlClassDefAttribute[])type.GetCustomAttributes(typeof(XmlClassDefAttribute), true);

                List<PropertyInfo> properties = new List<PropertyInfo>();

                for (int i = 0; i < propertiesInfo.Length; i++)
                {
                    XmlExportAttribute[] attribs = (XmlExportAttribute[])propertiesInfo[i].GetCustomAttributes(typeof(XmlExportAttribute), true);

                    if (PropertyIsExported(obj, propertiesInfo[i], attribs))
                    {
                        properties.Add(propertiesInfo[i]);
                    }
                }

                if (classAttribs.Length > 0 && classAttribs[0].Ordering == XmlClassDefOrdering.UserDefined)
                    properties.Sort(PropertyComparer);

                if (obj != null)
                {
                    WriteObjectPropertiesToXml(obj, properties, xmlWriter);
                }

                if (collection != null)
                {
                    foreach (object child in collection)
                    {
                        Type childType = child.GetType();

                        xmlWriter.WriteStartElement(childType.Name);

                        if (IsComplexType(childType))
                        {
                            if (_referenceTable.ContainsKey(child))
                            {
                                xmlWriter.WriteAttributeString("refid", _referenceTable[child].ToString(CultureInfo.CurrentCulture));
                            }
                            else
                            {
                                int id = _referenceTable.Count;
                                _referenceTable.Add(child, id);
                                xmlWriter.WriteAttributeString("id", id.ToString(CultureInfo.CurrentCulture));

                                ToXml(child, xmlWriter);
                            }
                        }
                        else
                        {
                            xmlWriter.WriteString(child.ToString());
                        }

                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        /// <summary>
        /// Writes the properies associated with the specified rootObject that are contained in
        /// the specified PropertyInfo collection
        /// </summary>
        /// <param name="rootObject">
        /// The instance object whose properties will be enumerated.
        /// </param>
        /// <param name="properties">
        /// The collection of PropertyInfo objects associated with the rootObject.
        /// </param>
        /// <param name="xmlWriter">
        /// The XmlTextWriter output stream.
        /// </param>
        private void WriteObjectPropertiesToXml(object rootObject, IEnumerable<PropertyInfo> properties,
            XmlTextWriter xmlWriter)
        {
            Type rootObjectType = rootObject.GetType();

            if (rootObjectType.Equals(runtimeType) && rootObjectType.IsGenericParameter == false)
            {
                xmlWriter.WriteAttributeString("type", rootObjectType.FullName);
                xmlWriter.WriteAttributeString("value", rootObject.ToString());
                return;
            }

            foreach (PropertyInfo propertyInfo in properties)
            {
                XmlExportAttribute[] attribs = (XmlExportAttribute[])
                    propertyInfo.GetCustomAttributes(typeof(XmlExportAttribute), true);

                bool ignoreIfNull = attribs.Count() > 0 && attribs[0].IgnoreIfNullOrEmpty;

                object propValue = propertyInfo.GetValue(rootObject, null);

                IEnumerable collection = propValue as IEnumerable;

                if (collection != null)
                {
                    if (Tools.CountIEnumerable(collection) < 1 && ignoreIfNull)
                    {
                        continue;
                    }
                }

                if (propValue == null)
                {
                    if (rootObject is IEnumerable || ignoreIfNull)
                    {
                        continue;
                    }
                    else
                    {
                        propValue = String.Empty;
                    }
                }

                // handle properties that are defined as Attributes or Literals
                if (attribs.Length > 0 && HandleNonStandardExportProperties(propValue, propertyInfo, xmlWriter, attribs[0]))
                {
                    continue;
                }

                Type propertyType = propValue.GetType();

                if (attribs.Length < 1 || !(attribs[0].PrintMembersOnly))
                {
                    xmlWriter.WriteStartElement(((attribs.Length > 0 && !String.IsNullOrEmpty(
                        attribs[0].ExportName)) ?
                        attribs[0].ExportName :
                        propertyInfo.Name));
                }

                if (propValue is DateTime)
                {
                    HandleDateTimeProperty((DateTime)propValue, (attribs.Length > 0) ? attribs[0] : null, xmlWriter);
                }
                else if (propValue is DateTimeOffset)
                {
                    HandleDateTimeProperty((DateTimeOffset)propValue, (attribs.Length > 0) ? attribs[0] : null, xmlWriter);
                }
                else if (IsComplexType(propertyType))
                {
                    HandleComplexType(propValue, _referenceTable, xmlWriter);
                }
                else
                {
                    xmlWriter.WriteString(propValue.ToString());
                }

                if (attribs.Length < 1 || !(attribs[0].PrintMembersOnly))
                {
                    xmlWriter.WriteEndElement();
                }
            }
        }

        private void HandleComplexType(Object propertyValue, IDictionary<Object, Int32> referenceTable, XmlTextWriter xmlWriter)
        {
            if (referenceTable.ContainsKey(propertyValue))
            {
                xmlWriter.WriteAttributeString("refid", referenceTable[propertyValue].ToString(CultureInfo.CurrentCulture));
            }
            else
            {
                int id = referenceTable.Count;
                referenceTable.Add(propertyValue, id);
                xmlWriter.WriteAttributeString("id", id.ToString(CultureInfo.CurrentCulture));

                ToXml(propertyValue, xmlWriter);
            }
        }

        private static void HandleDateTimeProperty(DateTime property, XmlExportAttribute attrib, XmlTextWriter xmlWriter)
        {
            if (attrib == null || String.IsNullOrEmpty(attrib.DateTimeFormat))
            {
                xmlWriter.WriteString(property.ToString(CultureInfo.CurrentCulture));
            }
            else
            {
                xmlWriter.WriteString(property.ToString(attrib.DateTimeFormat, CultureInfo.CurrentCulture));
            }
        }

        private static void HandleDateTimeProperty(DateTimeOffset property, XmlExportAttribute attrib, XmlTextWriter xmlWriter)
        {
            if (attrib == null || String.IsNullOrEmpty(attrib.DateTimeFormat))
            {
                xmlWriter.WriteString(property.ToString(CultureInfo.CurrentCulture));
            }
            else
            {
                xmlWriter.WriteString(property.ToString(attrib.DateTimeFormat, CultureInfo.CurrentCulture));
            }
        }

        private bool HandleNonStandardExportProperties(object propValue, PropertyInfo propertyInfo, 
                                                              XmlTextWriter xmlWriter, XmlExportAttribute attrib)
        {
            switch (attrib.XmlExportChildType)
            {
                case XmlExportChildType.Attribute:
                    ToXmlAttribute(propValue, propertyInfo, attrib, xmlWriter);
                    return true;
                case XmlExportChildType.Literal:
                    ToXmlLiteral(propValue, attrib, xmlWriter);
                    return true;
                case XmlExportChildType.Comment:
                    ToXmlComment(propValue, propertyInfo, attrib, xmlWriter);
                    return true;
                default:
                    return false;
            }
        }

        private static void ToXmlAttribute<T>(T value, PropertyInfo property, XmlExportAttribute attribs,
            XmlWriter xmlWriter)
        {
            // write the attribute name
            xmlWriter.WriteStartAttribute(((String.IsNullOrEmpty(attribs.ExportName)) ?
                property.Name : attribs.ExportName));

            // write the attribute value
            xmlWriter.WriteString(value.ToString());
            xmlWriter.WriteEndAttribute();
        }

        private static void ToXmlLiteral<T>(T value, XmlExportAttribute attribs, XmlWriter xmlWriter)
        {
            // Handle specified DateTime format, if any, otherwise use 
            // default string casting behavior for the literal value
            if (value is DateTime)
            {
                DateTime date = Tools.Convert(value, default(DateTime));

                if (String.IsNullOrEmpty(attribs.DateTimeFormat))
                {
                    xmlWriter.WriteString(date.ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    xmlWriter.WriteString(date.ToString(attribs.DateTimeFormat,
                        CultureInfo.CurrentCulture));
                }
            }
            else if (value is DateTimeOffset)
            {
                DateTimeOffset date = Tools.Convert(value, default(DateTimeOffset));

                if (String.IsNullOrEmpty(attribs.DateTimeFormat))
                {
                    xmlWriter.WriteString(date.ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    xmlWriter.WriteString(date.ToString(attribs.DateTimeFormat,
                        CultureInfo.CurrentCulture));
                }
            }
            else
            {
                xmlWriter.WriteString(value.ToString());
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private void ToXmlComment<T>(T value, PropertyInfo property, XmlExportAttribute attribs,
            XmlWriter xmlWriter)
        {
            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
            {
                using (XmlTextWriter commentWriter = new XmlTextWriter(sw))
                {
                    commentWriter.Formatting = Formatting.Indented;
                    commentWriter.Indentation = 5;

                    commentWriter.WriteWhitespace(Environment.NewLine);

                    if (IsComplexType(value.GetType()))
                    {
                        commentWriter.WriteStartElement(((String.IsNullOrEmpty(attribs.ExportName)) ?
                            property.Name : attribs.ExportName));

                        ToXml(value, commentWriter);
                        commentWriter.WriteEndElement();
                    }
                    else
                    {
                        commentWriter.WriteString(value.ToString());
                    }

                    commentWriter.WriteWhitespace(Environment.NewLine);

                    xmlWriter.WriteComment(sw.GetStringBuilder().ToString());
                    xmlWriter.WriteWhitespace("\n");
                }
            }
        }

        /// <summary>
        /// Determines whether the specified type is considered a "Complex Type" for the
        /// purposes of the ToXml export algorithm.  Complex Types would be any types that
        /// are not considered "Simple Types": Primitives, Strings, DateTimes, Decimals,
        /// and Enums.
        /// </summary>
        /// <param name="type">The <see cref="System.Type" /> to test.</param>
        /// <returns>
        /// 	<c>true</c> if [the specified type] is a complex type; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsComplexType(Type type)
        {
            return (!type.IsPrimitive && !(type.Equals(typeof(string))) &&
                    !(type.Equals(typeof(DateTime))) && !(type.Equals(typeof(decimal))) &&
                    !(type.IsEnum) && !(type.Equals(typeof(Guid))));
        }

        /// <summary>
        /// Determines whether or not the property associated with the
        /// specified instance should be exported to Xml.
        /// </summary>
        /// <param name="obj">
        /// The instance of the object that is being exported.
        /// </param>
        /// <param name="property">
        /// The property to evalutate.
        /// </param>
        /// <param name="attribs">
        /// The XmlExportAttribute array associated with the property.
        /// </param>
        /// <returns>
        /// Returns true if the property should be exported.  Returns false if it should be ignored.
        /// </returns>
        private static bool PropertyIsExported<T>(T obj, PropertyInfo property,
            XmlExportAttribute[] attribs)
        {
            // The method for determining how a property is exported is dependent upon
            // whether or not the object implements the IList interface
            if (obj is IEnumerable)
            {
                return (((attribs.Length > 0) ? attribs[0].IsExported : false) && property.CanRead);
            }
            else
            {
                return (property.CanRead && ((attribs.Length > 0) ? attribs[0].IsExported : true));
            }
        }

        /// <summary>
        /// Compares two PropertyInfo instances
        /// </summary>
        /// <param name="p1">The left PropertyInfo.</param>
        /// <param name="p2">The right PropertyInfo.</param>
        /// <returns>
        /// If the return value is less than zero, the left property is the lesser property.
        /// If the return value is equal to zero, the properties are equivalent (for the purpose
        /// of sorting precedence).
        /// If the return value is greater than zero, the left property is the greater property.
        /// </returns>
        private int PropertyComparer(PropertyInfo p1, PropertyInfo p2)
        {
            XmlExportAttribute[] attribs1 = (XmlExportAttribute[])p1.GetCustomAttributes(typeof(XmlExportAttribute), true);
            XmlExportAttribute[] attribs2 = (XmlExportAttribute[])p2.GetCustomAttributes(typeof(XmlExportAttribute), true);

            XmlExportAttribute a1 = (attribs1.Length > 0) ? attribs1[0] : null;
            XmlExportAttribute a2 = (attribs2.Length > 0) ? attribs2[0] : null;

            if (a1 != null && a2 != null)
            {
                return ((a1 < a2) ? -1 : (a1 > a2) ? 1 : 0);
            }
            else if (a1 != null)
            {
                return 1;
            }
            else if (a2 != null)
            {
                return -1;
            }
            else
                return 0;
        }

        #endregion

        #endregion
    }
}