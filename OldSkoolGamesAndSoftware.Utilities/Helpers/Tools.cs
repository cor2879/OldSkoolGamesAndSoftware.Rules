using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Threading;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines a class that contains Helper methods for various tasks.
    /// </summary>
    public static class Tools
    {
        #region Convert<TInput, TOutput>

        /// <summary>
        /// Converts an object of type T to an object of type K.
        /// </summary>
        /// <typeparam name="TInput">
        /// The System.Type of the object to convert.
        /// </typeparam>
        /// <typeparam name="TOutput">
        /// The System.Type to convert the object to.
        /// </typeparam>
        /// <param name="obj">
        /// The Object to convert.
        /// </param>
        /// <returns>
        /// The K equivalent of the supplied T value.  If conversion fails, returns
        /// the default value of K.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static TOutput Convert<TInput, TOutput>(TInput obj)
        {
            return Convert<TInput, TOutput>(obj, default(TOutput));
        }

        /// <summary>
        /// Converts an object of type T to an object of type K.
        /// </summary>
        /// <typeparam name="TInput">
        /// The System.Type of the object to convert.  
        /// </typeparam>
        /// <typeparam name="TOutput">
        /// The System.Type to convert the object to.
        /// </typeparam>
        /// <param name="obj">
        /// The Object to convert.
        /// </param>
        /// <param name="defaultValue">
        /// The value to return if conversion fails.
        /// </param>
        /// <returns>
        /// The K equivalent of the supplied T value.  If conversion fails, returns
        /// defaultValue.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static TOutput Convert<TInput, TOutput>(TInput obj, TOutput defaultValue)
        {
            try
            {
                return (TOutput)System.Convert.ChangeType(obj, typeof(TOutput),
                    CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        #endregion

        #region Min<T>, Max<T>

        /// <summary>
        /// Returns whichever one of two items has the highest value as determined by their
        /// IComparable interface implementation.
        /// </summary>
        /// <param name="lhs">The first item.</param>
        /// <param name="rhs">The second item.</param>
        /// <returns>
        /// Returns the greater of the two values.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", 
            "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rhs")]
        public static T Max<T>(T lhs, T rhs) where T : IComparable
        {
            return (lhs.CompareTo(rhs) > 0) ? lhs : rhs;
        }

        /// <summary>
        /// Returns whichever one of two items has the highest value as determined by the specified
        /// Comparison delegate.
        /// </summary>
        /// <param name="lhs">The first item.</param>
        /// <param name="rhs">The second item.</param>
        /// <param name="comparer">
        /// A delegate instance that will be used to compare the elements.
        /// </param>
        /// <returns>
        /// The greater of the two values, based on the comparison performed by the delegate.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rhs")]
        public static T Max<T>(T lhs, T rhs, Comparison<T> comparer)
        {
            if (comparer == null)
            {
                return default(T);
            }

            return (comparer.Invoke(lhs, rhs) > 0) ? lhs : rhs;
        }

        /// <summary>
        /// Returns whichever one of two items has the lowest value as determined by their
        /// IComparable interface implementation.
        /// </summary>
        /// <param name="lhs">The first item.</param>
        /// <param name="rhs">The second item.</param>
        /// <returns>
        /// Returns the lesser of the two items.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rhs")]
        public static T Min<T>(T lhs, T rhs) where T : IComparable
        {
            return (lhs.CompareTo(rhs) < 0) ? lhs : rhs;
        }

        /// <summary>
        /// Returns whichever one of two items has the lowest value as determined by their
        /// IComparable interface implementation.
        /// </summary>
        /// <param name="lhs">The first item.</param>
        /// <param name="rhs">The second item.</param>
        /// <param name="comparer">
        /// A delegate instance that will be used to compare the elements.
        /// </param>
        /// <returns>
        /// Returns the lesser of the two items.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rhs")]
        public static T Min<T>(T lhs, T rhs, Comparison<T> comparer)
        {
            if (comparer == null)
            {
                return default(T);
            }

            return (comparer.Invoke(lhs, rhs) < 0) ? lhs : rhs;
        }

        #endregion

        #region Ternary<T>

        /// <summary>
        /// Helper method that makes the Ternary operator
        /// available to VB code.
        /// </summary>
        /// <typeparam name="T">
        /// The Type that should be returned by the Ternary operation.
        /// </typeparam>
        /// <param name="condition">
        /// The Boolean Statement to be evaluated by the Ternary operation.
        /// </param>
        /// <param name="ifTrue">
        /// The value to return if the Boolean statement evaluates to true.
        /// </param>
        /// <param name="ifFalse">
        /// The value to return if the Boolean statement evaluates to false.
        /// </param>
        public static T Ternary<T>(bool condition, T ifTrue, T ifFalse)
        {
            return ((condition) ? ifTrue : ifFalse);
        }

        #endregion

        #region CountIEnumerable

        /// <summary>
        /// Counts the number of elements in the specified IEnumerable object.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the parameter 'collection' is null.
        /// </exception>"
        public static int CountIEnumerable(IEnumerable collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "The parameter 'collection' may not be null.");
            }

            int count = 0;

            IEnumerator enumerator = collection.GetEnumerator();

            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    count++;
                }
            }

            return count;
        }

        #endregion

        #region Retry Method

        /// <summary>
        /// Retry specified times when operation failed.
        /// </summary>
        /// <param name="action">delegate for retry when failed.</param>
        /// <param name="maxRetryTimes">max retry times</param>
        /// <param name="interval">set the interval for retry operation</param>
        public static void RetryIfFailed(Action action, int maxRetryTimes, int interval)
        {
            Exception exception = null;
            bool success = false;
            while (maxRetryTimes >= 0 && !success)
            {
                try
                {
                    action();
                    success = true;
                }
                catch (Exception ex)
                {
                    if (maxRetryTimes > 0)
                        Thread.Sleep(interval);         //we don't need to sleep if this is the last retry.
                    maxRetryTimes--;
                    exception = ex;
                }
            }

            if (exception != null && !success)
            {
                throw exception;
            }
        }

        #endregion

#if SILVERLIGHT
        #region Enum Helpers

        /// <summary>
        /// Gets the names of all available options for an Enumerated type.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static string[] GetNames(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType", "The parameter 'enumType' may not be null.");
            }

            if (!(enumType.IsEnum))
            {
                throw new ArgumentException("The parameter 'enumType' must be an instance of System.Type that represents a type of Enum.", "enumType");
            }

            FieldInfo[] fieldInfoArray = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);
            string[] names = new string[fieldInfoArray.Length];

            for (int i = 0; i < fieldInfoArray.Length; i++)
            {
                names[i] = fieldInfoArray[i].Name;
            }

            return names;
        }

        /// <summary>
        /// Gets the available values associated with an Enumerated type.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static Enum[] GetValues(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType", "The parameter 'enumType' may not be null.");
            }

            if (!(enumType.IsEnum))
            {
                throw new ArgumentException("The parameter 'enumType' must be an instance of System.Type that represents a type of Enum.", "enumType");
            }

            FieldInfo[] fieldInfoArray = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            Enum[] list = new Enum[fieldInfoArray.Length];

            for (int i = 0; i < fieldInfoArray.Length; i++)
            {
                list[i] = (Enum)Enum.Parse(enumType, fieldInfoArray[i].Name, false);
            }

            return list;
        }

        #endregion
#endif
    }
}