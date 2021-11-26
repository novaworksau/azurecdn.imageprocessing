// file:	EParliament.AzureCdnImageProvider.AspNet\ServiceCollectionExtensions.cs
//
// summary:	Implements the service collection extensions class
using Microsoft.Extensions.DependencyInjection;

namespace EParliament.AzureCdnImageProvider.AspNet
{
    /// <summary>
    /// A service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// An IServiceCollection extension method that configure parliament azure cdn middleware.
        /// </summary>
        /// <exception cref="Exception"> Thrown when an exception error condition occurs.</exception>
        /// <param name="services"> The services to act on.</param>
        /// <param name="builder"> The builder.</param>
        /// <returns>
        /// An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureEParliamentAzureCdnMiddleware(this IServiceCollection services, Action<IEParliamentCdnMiddlewareBuilder> builder)
        {
            var oBuilder = new EParliamentCdnMiddlewareBuilder();

            builder.Invoke(oBuilder);

            if (oBuilder.BlobStorageOptions == null)
            {
                throw new Exception($"The blob storage options are not configured.");
            }

            oBuilder.BlobStorageOptions.Validate();

            if (string.IsNullOrEmpty(oBuilder.PathPrefix))
            {
                throw new Exception($"The base path is not configured.");
            }
            var middlewareOptions = new EParliamentMiddlewareOptions { PathPrefix = oBuilder.PathPrefix };

            services.AddSingleton(oBuilder.BlobStorageOptions)
                    .AddSingleton(middlewareOptions)
                    .AddSingleton<IBlobClientService, BlobClientService>();
            return services;
        }
    }
}