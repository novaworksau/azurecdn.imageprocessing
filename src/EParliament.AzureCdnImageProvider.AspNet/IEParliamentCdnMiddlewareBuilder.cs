// file:	EParliament.AzureCdnImageProvider.AspNet\IEParliamentCdnMiddlewareBuilder.cs
//
// summary:	Implements the IE parliament cdn middleware builder class
using Microsoft.Extensions.Configuration;

namespace EParliament.AzureCdnImageProvider.AspNet
{
    /// <summary>
    /// Interface for IE parliament cdn middleware builder.
    /// </summary>
    public interface IEParliamentCdnMiddlewareBuilder
    {
        /// <summary>
        /// With azure BLOB storage options.
        /// </summary>
        /// <param name="configuration"> The configuration.</param>
        /// <param name="configurationKey"> The configuration key.</param>
        /// <returns>
        /// An IEParliamentCdnMiddlewareBuilder.
        /// </returns>
        IEParliamentCdnMiddlewareBuilder WithAzureBlobStorageOptions(IConfiguration configuration, string configurationKey);

        /// <summary>
        /// With azure BLOB storage options.
        /// </summary>
        /// <param name="options"> Options for controlling the operation.</param>
        /// <returns>
        /// An IEParliamentCdnMiddlewareBuilder.
        /// </returns>
        IEParliamentCdnMiddlewareBuilder WithAzureBlobStorageOptions(AzureBlobStorageOptions options);

        /// <summary>
        /// With path prefix.
        /// </summary>
        /// <param name="basePath"> Full pathname of the base file.</param>
        /// <returns>
        /// An IEParliamentCdnMiddlewareBuilder.
        /// </returns>
        IEParliamentCdnMiddlewareBuilder WithPathPrefix(string basePath);
    }
}