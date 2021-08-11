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
        /// <summary>
        /// Replaces the default component activator with <see cref="AppRegPortal.Utilities.DIComponentActivator"/>
        /// This will allow use of constructor injection in the components.
        /// </summary>
        /// <param name="services">Container</param>
        public static void UseDIComponentActivator(this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            services.Replace(ServiceDescriptor.Transient<IComponentActivator, DIComponentActivator>());
        }

        /// <summary>
        /// Automatically register all classes that implement IComponent with the contianer that implements
        /// <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type who's assemply will be searched</typeparam>
        /// <param name="services">Container</param>
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
