using System;

namespace OldSkoolGamesAndSoftware.Utilities
{
    /// <summary>
    /// Provides a callback pattern for Methods which process and return results
    /// from an Asynchronous operation.
    /// </summary>
    /// <typeparam name="TData">
    /// The return type of the method.
    /// </typeparam>
    /// <param name="result">
    /// The IAsyncResult instance representing the state of the asynchronous operation.
    /// </param>
    /// <returns>
    /// Returns the result of the completed asynchronous operation.
    /// </returns>
    public delegate TData AsyncResultCallback<TData>(IAsyncResult result);
}