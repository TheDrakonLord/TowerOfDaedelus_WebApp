using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TowerOfDaedalus_WebApp_Arango
{
    public static class ArangoServiceCollectionExtensions
    {
        public static IServiceCollection AddArangoConfig(
            this IServiceCollection services, IConfiguration config)
        {
            /**services.Configure<PositionOptions>(
                config.GetSection(PositionOptions.Position));
            services.Configure<ColorOptions>(
                config.GetSection(ColorOptions.Color));**/

            return services;
        }

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
