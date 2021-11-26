// file:	EParliament.AzureCdnImageProvider\BlobClientService.cs
//
// summary:	Implements the BLOB client service class
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace EParliament.AzureCdnImageProvider
{
    /// <summary>
    /// A service for accessing BLOB clients information.
    /// </summary>
    public class BlobClientService : IBlobClientService
    {
        /// <summary>
        /// (Immutable) the endpoint suffix.
        /// </summary>
        private const string ENDPOINT_SUFFIX = "blob.core.windows.net";

        /// <summary>
        /// (Immutable) the logger.
        /// </summary>
        private readonly ILogger<BlobClientService> _logger;

        /// <summary>
        /// (Immutable) options for controlling the operation.
        /// </summary>
        private readonly AzureBlobStorageOptions _options;

        /// <summary>
        /// The BLOB service client.
        /// </summary>
        private BlobContainerClient? _blobServiceClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"> Options for controlling the operation.</param>
        /// <param name="logger"> The logger.</param>
        public BlobClientService(AzureBlobStorageOptions options, ILogger<BlobClientService> logger)
        {
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Gets the container client.
        /// </summary>
        /// <value>
        /// The container client.
        /// </value>
        public BlobContainerClient ContainerClient => GetBlobContainerClient();

        /// <summary>
        /// Gets BLOB container client.
        /// </summary>
        /// <returns>
        /// The BLOB container client.
        /// </returns>
        private BlobContainerClient GetBlobContainerClient()
        {
            if (_blobServiceClient == null)
            {
                var endPointSuffix = (_options.EndPointSuffix ?? ENDPOINT_SUFFIX).TrimStart('.');
                var uri = new Uri($"https://{_options.AccountName}.{endPointSuffix}");
                BlobServiceClient client;
                if (_options.AuthMode == AzureBlobStorageAuthMode.AccountKey)
                {
                    var credential = new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey);
                    client = new BlobServiceClient(uri, credential);
                }
                else
                {
                    client = new BlobServiceClient(uri, new DefaultAzureCredential());
                }
                _blobServiceClient = client.GetBlobContainerClient(_options.ContainerName);
            }
            return _blobServiceClient;
        }
    }
}