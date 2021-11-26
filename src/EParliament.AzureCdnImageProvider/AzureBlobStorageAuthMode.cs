// file:	EParliament.AzureCdnImageProvider\AzureBlobStorageAuthMode.cs
// summary:	Implements the azure BLOB storage authentication mode class

namespace EParliament.AzureCdnImageProvider
{
    /// <summary>
    /// Values that represent azure BLOB storage Authentication modes.
    /// </summary>
    public enum AzureBlobStorageAuthMode
    {
        /// <summary>
        /// An enum constant representing the account key option.
        /// </summary>
        AccountKey = 0,

        /// <summary>
        /// An enum constant representing the managed identity option.
        /// </summary>
        ManagedIdentity = 1
    }
}