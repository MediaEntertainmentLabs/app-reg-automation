using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;

namespace AppRegShared.Utility
{
    public static class Guard
    {
        public static T NotNull<T>(T obj, string name, ILogger logger = null)
        {
            if(obj == null)
            {
                string message = $"${name} can not be null";
                logger?.LogError(message);
                throw new ArgumentNullException(name, message);
            }
            return obj;
        }
    }
}
