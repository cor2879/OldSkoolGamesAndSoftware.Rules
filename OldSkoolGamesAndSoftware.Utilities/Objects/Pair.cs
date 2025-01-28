using System;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Defines a generic pair of objects.
    /// </summary>
    /// <typeparam name="TFirst">The type of the first.</typeparam>
    /// <typeparam name="TSecond">The type of the second.</typeparam>
    public struct Pair<TFirst, TSecond>
        : IEquatable<Pair<TFirst, TSecond>>
    {
        #region Fields

        private TFirst _first;
        private TSecond _second;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;TFirst, TSecond&gt;"/> struct.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public Pair(TFirst first, TSecond second)
        {
            _first = first;
            _second = second;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        /// <value>The first.</value>
        public TFirst First
        {
            get { return _first; }
            set { _first = value; }
        }

        /// <summary>
        /// Gets or sets the second.
        /// </summary>
        /// <value>The second.</value>
        public TSecond Second
        {
            get { return _second; }
            set { _second = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Pair<TFirst, TSecond>))
            {
                return false;
            }

            return Equals((Pair<TFirst, TSecond>)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="OldSkoolGamesAndSoftware.Utilities.Pair&lt;TFirst, TSecond>" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="OldSkoolGamesAndSoftware.Utilities.Pair&lt;TFirst, TSecond>" /> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="OldSkoolGamesAndSoftware.Utilities.Pair&lt;TFirst, TSecond>" /> is equal to this instance; otherwise, 
        /// 	<c>false</c>.
        /// </returns>
        public bool Equals(Pair<TFirst, TSecond> other)
        {
            return First.Equals(other.First) && Second.Equals(other.Second);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (First.Equals(default(TFirst)) ? 0 : First.GetHashCode()) ^
                (Second.Equals(default(TSecond)) ? 0 : Second.GetHashCode());
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Pair<TFirst, TSecond> lhs, Pair<TFirst, TSecond> rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Pair<TFirst, TSecond> lhs, Pair<TFirst, TSecond> rhs)
        {
            return !(lhs.Equals(rhs));
        }

        #endregion
    }
}