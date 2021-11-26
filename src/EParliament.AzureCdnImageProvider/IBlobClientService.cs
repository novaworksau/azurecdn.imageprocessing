// file:	EParliament.AzureCdnImageProvider\BlobClientService.cs
//
// summary:	Implements the BLOB client service class
using Azure.Storage.Blobs;

namespace EParliament.AzureCdnImageProvider
{
    /// <summary>
    /// Interface for BLOB client service.
    /// </summary>
    public interface IBlobClientService
    {
        /// <summary>
        /// Gets the container client.
        /// </summary>
        /// <value>
        /// The container client.
        /// </value>
        BlobContainerClient ContainerClient { get; }
    }
}