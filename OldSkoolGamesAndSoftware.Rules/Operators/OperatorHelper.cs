// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorHelper.cs" company="Old Skool Games and Software">
//   Copyright © 2025 Old Skool Games And Software
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace OldSkoolGamesAndSoftware.Rules.Operators
{
    using System;

    /// <summary>
    /// Exposes methods and helpers for dealing with Expression Operators.
    /// </summary>
    public static class OperatorHelper
    {
        #region Methods

        /// <summary>
        /// Gets the binary operator from ude operator code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The <see cref="BinaryOperator" /> that corresponds to the supplied code.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown if the supplied code does not match any existing Binary operator ID.
        /// </exception>
        public static BinaryOperator GetBinaryOperatorFromUdeOperatorCode(int code)
        {
            switch (code)
            {
                case 0:
                    return BinaryOperatorManager.Equal;
                case 1:
                    return BinaryOperatorManager.NotEqual;
                case 2: 
                    return BinaryOperatorManager.LessThan;
                case 3:
                    return BinaryOperatorManager.LessThanOrEqual;
                case 4:
                    return BinaryOperatorManager.GreaterThan;
                case 5:
                    return BinaryOperatorManager.GreaterThanOrEqual;
                case 6:
                    return BinaryOperatorManager.RegExMatch;
                default:
                    throw new Exception(
                        string.Format("No matching Binary Operator for ID {0}", code));
            }
        }

        /// <summary>
        /// Gets the set operator from ude operator code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The <see cref="SetOperator" /> that corresponds to the supplied code.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown if the supplied Id does not match any known Set Operator.
        /// </exception>
        public static SetOperator GetSetOperatorFromUdeOperatorCode(int code)
        {
            switch (code)
            {
                case 6:
                    return SetOperatorManager.Exists;
                default:
                    throw new Exception(
                        string.Format("No matching Set Operator for ID {0}", code));
            }
        }

        #endregion
    }
}