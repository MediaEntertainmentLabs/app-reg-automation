using AppRegShared.Utility;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using System;

namespace AppRegPortal.Utilities
{
    /// <summary>
    /// Custom Component activator to use Dependency Injection container for creating components
    /// See: https://github.com/dotnet/aspnetcore/issues/18088
    /// https://github.com/dotnet/aspnetcore/blob/main/src/Components/Components/src/DefaultComponentActivator.cs
    /// </summary>
    public class DIComponentActivator : IComponentActivator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public DIComponentActivator(IServiceProvider serviceProvider, ILogger<DIComponentActivator> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._serviceProvider = Guard.NotNull(serviceProvider, nameof(serviceProvider), logger);
        }

        public IComponent CreateInstance(Type componentType)
        {
            if (!typeof(IComponent).IsAssignableFrom(componentType))
            {
                string message = $"The type {componentType.FullName} does not implement {nameof(IComponent)}.";
                this._logger.LogError(message);
                throw new ArgumentException(message, nameof(componentType));
            }

            object? instance = this._serviceProvider.GetService(componentType)
                              ?? Activator.CreateInstance(componentType);

            var component = instance as IComponent;
            if (component == null)
            {
                throw new ArgumentException($"Unable to create an instance of {componentType.FullName}");
            }

            return (IComponent)instance!;
        }
    }
}
