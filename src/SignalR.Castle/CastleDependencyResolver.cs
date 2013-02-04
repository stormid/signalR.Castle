using System;
using System.Collections.Generic;
using Castle.MicroKernel;
using Microsoft.AspNet.SignalR;

namespace SignalR.Castle
{
    /// <summary>
    /// Castle Windsor implementation of the <see cref="Microsoft.AspNet.SignalR.IDependencyResolver"/> interface.
    /// </summary>
    public class CastleDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel kernel;

        public CastleDependencyResolver(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");

            this.kernel = kernel;
        }

        /// <summary>
        /// Gets the Castle Windsor implementation of the dependency resolver.
        /// </summary>
        public static CastleDependencyResolver Current
        {
            get { return GlobalHost.DependencyResolver as CastleDependencyResolver; }
        }

        /// <summary>
        /// Get a single instance of a service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The single instance if resolved; otherwise, <c>null</c>.</returns>
        public override object GetService(Type serviceType)
        {
            if ( kernel.HasComponent(serviceType))
                return kernel.Resolve(serviceType);

            return null;
        }

        /// <summary>
        /// Gets all available instances of a services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The list of instances if any were resolved; otherwise, an empty list.</returns>
        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return (IEnumerable<object>)kernel.ResolveAll(serviceType);
        }
    }
}