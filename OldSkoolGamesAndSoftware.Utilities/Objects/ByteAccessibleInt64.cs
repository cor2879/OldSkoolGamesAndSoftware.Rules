// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ByteAccessibleInt64.cs" company="Old Skool Games and Software">
//   Copyright © 2024 Old Skool Games and Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace OldSkoolGamesAndSoftware.Utilities
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// An 64-bit integer that exposes each of its 8 bytes through individual getters.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ByteAccessibleInt64
    {
        #region Fields

        /// <summary>
        /// The value
        /// </summary>
        [FieldOffset(0)]
        private long value;

        /// <summary>
        /// The byte0
        /// </summary>
        [FieldOffset(7)]
        private byte byte0;

        /// <summary>
        /// The byte1
        /// </summary>
        [FieldOffset(6)]
        private byte byte1;

        /// <summary>
        /// The byte2
        /// </summary>
        [FieldOffset(5)]
        private byte byte2;

        /// <summary>
        /// The byte3
        /// </summary>
        [FieldOffset(4)]
        private byte byte3;

        /// <summary>
        /// The byte4
        /// </summary>
        [FieldOffset(3)]
        private byte byte4;

        /// <summary>
        /// The byte5
        /// </summary>
        [FieldOffset(2)]
        private byte byte5;

        /// <summary>
        /// The byte6
        /// </summary>
        [FieldOffset(1)]
        private byte byte6;

        /// <summary>
        /// The byte7
        /// </summary>
        [FieldOffset(0)]
        private byte byte7;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public long Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Gets the byte0.
        /// </summary>
        /// <value>
        /// The byte0.
        /// </value>
        public byte Byte0
        {
            get { return this.byte0; }
        }

        /// <summary>
        /// Gets the byte1.
        /// </summary>
        /// <value>
        /// The byte1.
        /// </value>
        public byte Byte1
        {
            get { return this.byte1; }
        }

        /// <summary>
        /// Gets the byte2.
        /// </summary>
        /// <value>
        /// The byte2.
        /// </value>
        public byte Byte2
        {
            get { return this.byte2; }
        }

        /// <summary>
        /// Gets the byte3.
        /// </summary>
        /// <value>
        /// The byte3.
        /// </value>
        public byte Byte3
        {
            get { return this.byte3; }
        }

        /// <summary>
        /// Gets the byte4.
        /// </summary>
        /// <value>
        /// The byte4.
        /// </value>
        public byte Byte4
        {
            get { return this.byte4; }
        }

        /// <summary>
        /// Gets the byte5.
        /// </summary>
        /// <value>
        /// The byte5.
        /// </value>
        public byte Byte5
        {
            get { return this.byte5; }
        }

        /// <summary>
        /// Gets the byte6.
        /// </summary>
        /// <value>
        /// The byte6.
        /// </value>
        public byte Byte6
        {
            get { return this.byte6; }
        }

        /// <summary>
        /// Gets the byte7.
        /// </summary>
        /// <value>
        /// The byte7.
        /// </value>
        public byte Byte7
        {
            get { return this.byte7; }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int64"/> to <see cref="ByteAccessibleInt64"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ByteAccessibleInt64(long value)
        {
            var byteAccessibleInt64 = new ByteAccessibleInt64 {value = value};

            return byteAccessibleInt64;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ByteAccessibleInt64"/> to <see cref="System.Int64"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator long(ByteAccessibleInt64 value)
        {
            return value.Value;
        }

        #endregion
    }
}