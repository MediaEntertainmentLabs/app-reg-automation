using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics.CodeAnalysis;

namespace AppRegShared.Utility
{
    public static class Guard
    {
        public static T NotNull<T>([NotNull] T obj, string name, ILogger? logger = null)
        {
            if (obj == null)
            {
                string message = $"{name} can not be null";
                logger?.LogError(message);
                throw new ArgumentNullException(name, message);
            }
            return obj;
        }

        public static string NotNullOrEmpty([NotNull] string? obj, string name, ILogger? logger = null)
        {

            if (string.IsNullOrEmpty(obj))
            {
                string message = $"{name} can not be null or empty";
                logger?.LogError(message);
                throw new ArgumentException(message, name);
            }
            return obj;
        }
    }
}
