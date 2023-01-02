using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TowerOfDaedalus_WebApp_Arango
{
    /// <summary>
    /// Service collection extensions for dependency injection of arango utilities
    /// </summary>
    public static class ArangoServiceCollectionExtensions
    {
        /// <summary>
        /// Adds arango configuration options
        /// </summary>
        /// <param name="services">IServiceCollection called by dependency injection</param>
        /// <param name="config">IConfiguration called by dependency injection</param>
        /// <returns>IServiceCollection with Configuration options applied</returns>
        public static IServiceCollection AddArangoConfig(
            this IServiceCollection services, IConfiguration config)
        {
            /**services.Configure<PositionOptions>(
                config.GetSection(PositionOptions.Position));
            services.Configure<ColorOptions>(
                config.GetSection(ColorOptions.Color));**/

            return services;
        }

        /// <summary>
        /// adds arango dependency group
        /// </summary>
        /// <param name="services">IServiceCollection called by dependency injection</param>
        /// <returns>IServiceCollection with services added</returns>
        public static IServiceCollection AddArangoDependencyGroup(
            this IServiceCollection services)
        {
            /**services.AddScoped<IMyDependency, MyDependency>();
            services.AddScoped<IMyDependency2, MyDependency2>();**/

            services.AddScoped<IArangoUtils, Utilities>();

            return services;
        }
    }
}
