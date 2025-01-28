using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines a type that contains data to be output as an Xml Comment
    /// </summary>
    public abstract class XmlComment
    {
        #region Methods

        /// <summary>
        /// Gets the data enclosed in the appropriate comment tags.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public abstract string GetOuterXml();

        /// <summary>
        /// Gets the data without any comment tags.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public abstract string GetInnerXml();

        #endregion
    }

    /// <summary>
    /// Defines a type that contains data to be output as an Xml Comment
    /// </summary>
    /// <typeparam name="T">The type of the data to print as a comment.</typeparam>
    public class XmlComment<T> : XmlComment
    {
        #region Fields

        private T _innerValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlComment&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public XmlComment(T value)
        {
            Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return _innerValue; }
            set { _innerValue = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the data enclosed in the appropriate comment tags.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public override string GetOuterXml()
        {
            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(sw))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 5;

                    xmlWriter.WriteComment(GetInnerXml());
                }

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Gets the data without any comment tags.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public override string GetInnerXml()
        {
            using (StringWriter sw = new StringWriter(CultureInfo.CurrentCulture))
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(new StringWriter(CultureInfo.CurrentCulture)))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 5;

                    new XmlSerializer().ToXmlFragment(Value, xmlWriter);

                    return sw.GetStringBuilder().ToString();

                }
            }
        }

        #endregion
    }
}