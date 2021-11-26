// file:	EParliament.AzureCdnImageProvider.AspNet\EParliamentCdnMiddlewareBuilder.cs
//
// summary:	Implements the parliament cdn middleware builder class
using Microsoft.Extensions.Configuration;

namespace EParliament.AzureCdnImageProvider.AspNet
{
    /// <summary>
    /// A parliament cdn middleware builder.
    /// </summary>
    internal class EParliamentCdnMiddlewareBuilder : IEParliamentCdnMiddlewareBuilder
    {
        /// <summary>
        /// Full pathname of the base file.
        /// </summary>
        public string? PathPrefix;

        /// <summary>
        /// Options for controlling the BLOB storage.
        /// </summary>
        public AzureBlobStorageOptions? BlobStorageOptions;

        /// <summary>
        /// With azure BLOB storage options.
        /// </summary>
        /// <param name="options"> Options for controlling the operation.</param>
        /// <returns>
        /// An IEParliamentCdnMiddlewareBuilder.
        /// </returns>
        public IEParliamentCdnMiddlewareBuilder WithAzureBlobStorageOptions(AzureBlobStorageOptions options)
        {
            BlobStorageOptions = options;
            return this;
        }

        /// <summary>
        /// With azure BLOB storage options.
        /// </summary>
        /// <param name="configuration"> The configuration.</param>
        /// <param name="configurationKey"> The configuration key.</param>
        /// <returns>
        /// An IEParliamentCdnMiddlewareBuilder.
        /// </returns>
        public IEParliamentCdnMiddlewareBuilder WithAzureBlobStorageOptions(IConfiguration configuration, string configurationKey)
        {
            var options = new AzureBlobStorageOptions();
            configuration.Bind(configurationKey, options);
            return WithAzureBlobStorageOptions(options);
        }

        /// <summary>
        /// With base path.
        /// </summary>
        /// <param name="pathPrefix"> Full pathname of the base file.</param>
        /// <returns>
        /// An IEParliamentCdnMiddlewareBuilder.
        /// </returns>
        public IEParliamentCdnMiddlewareBuilder WithPathPrefix(string pathPrefix)
        {
            PathPrefix = pathPrefix.ToLowerInvariant();
            return this;
        }
    }
}