using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// A static helper class which contains methods, not found in the
    /// BitConverter class, for converting byte arrays into primitive 
    /// equivalents.
    /// </summary>
    public static class BinaryConverter
    {
        #region Fields

        private const byte MASK = 0xFF;
        private const long LowPartMask64 = 0x00000000FFFFFFFF;

        #endregion

        #region Methods

        /// <summary>
        /// Converts the specified byte array to a 32-bit signed integer
        /// in big endian format.
        /// </summary>
        /// <param name="array">The array of bytes to convert.</param>
        /// <returns>
        /// The 32-bit signed integer equivalent to the final four 
        /// (or fewer if the array length is less than four) bytes in the 
        /// array, in big endian format.
        /// </returns>
        public static Int32 ToInt32BigEndian(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array", "The parameter 'array' may not be null.");
            }

            Int32 value = ((array.Length > 0) ? array[array.Length - 1] : 0);

            for (int i = array.Length - 2; i > Math.Max(-1, array.Length - 5); i--)
            {
                value |= (((Int32)array[i]) << ((3 - i) << 3));
            }

            return value;
        }

        /// <summary>
        /// Converts the specified byte array to a 32-bit unsigned integer
        /// in big endian format.
        /// </summary>
        /// <param name="array">The array of bytes to convert.</param>
        /// <returns>
        /// The 32-bit unsigned integer equivalent to the final four 
        /// (or fewer if the array length is less than four) bytes in the 
        /// array, in big endian format.
        /// </returns>
        public static UInt32 ToUInt32BigEndian(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array", "The parameter 'array' may not be null.");
            }
            
            UInt32 value = ((array.Length > 0) ? array[array.Length - 1] : 0U);

            for (int i = array.Length - 2; i > Math.Max(-1, array.Length - 5); i--)
            {
                value |= (((UInt32)array[i]) << ((3 - i) << 3));
            }

            return value;
        }

        /// <summary>
        /// Converts the specified byte array to a 64-bit signed integer
        /// in big endian format.
        /// </summary>
        /// <param name="array">The array of bytes to convert.</param>
        /// <returns>
        /// The 64-bit signed integer equivalent to the final eight bytes in the 
        /// array, in big endian format.
        /// </returns>
        public static Int64 ToInt64BigEndian(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array", "The parameter 'array' may not be null.");
            }

            Int64 value = ((array.Length > 0) ? array[array.Length - 1] : 0);

            for (int i = array.Length - 2; i > Math.Max(-1, array.Length - 9); i--)
            {
                value |= (((Int64)array[i]) << ((7 - i) << 3));
            }

            return value;
        }

        /// <summary>
        /// Converts the specified byte array to a 64-bit unsigned integer
        /// in big endian format.
        /// </summary>
        /// <param name="array">The array of bytes to convert.</param>
        /// <returns>
        /// The 64-bit unsigned integer equivalent to the final eight bytes in the 
        /// array, in big endian format.
        /// </returns>
        public static UInt64 ToUInt64BigEndian(byte[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array", "The parameter 'array' may not be null.");
            }

            UInt64 value = ((array.Length > 0) ? array[array.Length - 1] : (UInt64)0);

            for (int i = array.Length - 2; i > Math.Max(-1, array.Length - 9); i--)
            {
                value |= (((UInt64)array[i]) << ((7 - i) << 3));
            }

            return value;
        }

        /// <summary>
        /// Converts the unsigned 64-bit integer parameter to a
        /// big endian 8-byte array.
        /// </summary>
        /// <param name="value">The unsigned 64-bit integer to convert.</param>
        /// <returns></returns>
        public static byte[] GetBytesBigEndian(UInt64 value)
        {
            byte[] b = new byte[8];

            b[7] = (byte)(value & MASK);

            for (int i = 6; i > -1; i--)
            {
                b[i] = (byte)(value >> ((7 - i) << 3) & MASK);
            }

            return b;
        }

        /// <summary>
        /// Converts the signed 64-bit integer parameter to a
        /// big endian 8-byte array.
        /// </summary>
        /// <param name="value">The signed 64-bit integer to convert.</param>
        /// <returns></returns>
        public static byte[] GetBytesBigEndian(Int64 value)
        {
            byte[] b = new byte[8];

            b[7] = (byte)(value & MASK);

            for (int i = 6; i > -1; i--)
            {
                b[i] = (byte)(value >> ((7 - i) << 3) & MASK);
            }

            return b;
        }

        /// <summary>
        /// Converts the signed 32-bit integer parameter to a
        /// big endian 4-byte array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytesBigEndian(Int32 value)
        {
            byte[] b = new byte[4];

            b[3] = (byte)(value & MASK);

            for (int i = 2; i > -1; i--)
            {
                b[i] = (byte)(value >> ((3 - i) << 3) & MASK);
            }

            return b;
        }

        /// <summary>
        /// Converts the unsigned 32-bit integer parameter to a
        /// big endian 4-byte array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytesBigEndian(UInt32 value)
        {
            byte[] b = new byte[4];

            b[3] = (byte)(value & MASK);

            for (int i = 2; i > -1; i--)
            {
                b[i] = (byte)(value >> ((3 - i) << 3) & MASK);
            }

            return b;
        }

        /// <summary>
        /// Returns the upper 32-bits of a 64-bit integer.
        /// </summary>
        /// <param name="value">
        /// The 64-bit integer.
        /// </param>
        /// <returns>
        /// The upper 32-bits of a 64-bit integer.
        /// </returns>
        public static int HighPart(long value)
        {
            return (int)(value >> 32);
        }

        /// <summary>
        /// Returns the lower 32-bits of a 64-bit integer.
        /// </summary>
        /// <param name="value">
        /// The 64-bit integer.
        /// </param>
        /// <returns>
        /// The lower 32-bits of a 64-bit integer.
        /// </returns>
        public static int LowPart(long value)
        {
            return (int)(value & LowPartMask64);
        }

        #endregion
    }
}
