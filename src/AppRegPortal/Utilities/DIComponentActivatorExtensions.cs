using AppRegShared.Utility;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Linq;
using System.Reflection;

namespace AppRegPortal.Utilities
{
    public static class DIComponentActivatorExtensions
    {
        public static void UseDIComponentActivator(this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            services.Replace(ServiceDescriptor.Transient<IComponentActivator, DIComponentActivator>());
        }

        public static void RegisterAllComponentsInImplementingAssembly<T>(this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            var assembly = Assembly.GetAssembly(typeof(T));

            assembly?
                .GetTypes()?
                .Where(t => typeof(IComponent).IsAssignableFrom(t) && t.IsClass)
                .ToList()
                .ForEach(t => services.AddTransient(t));
        }
    }
}
