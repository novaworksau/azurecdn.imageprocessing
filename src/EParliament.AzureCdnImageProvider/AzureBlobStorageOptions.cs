// file:	EParliament.AzureCdnImageProvider\AzureBlobStorageOptions.cs
//
// summary:	Implements the azure BLOB storage options class

namespace EParliament.AzureCdnImageProvider
{
    /// <summary>
    /// An azure BLOB storage options.
    /// </summary>
    public class AzureBlobStorageOptions
    {
        /// <summary>
        /// Gets or sets the account key.
        /// </summary>
        /// <value>
        /// The account key.
        /// </value>
        public string? AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string AccountName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the authentication mode.
        /// </summary>
        /// <value>
        /// The authentication mode.
        /// </value>
        public AzureBlobStorageAuthMode AuthMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the container.
        /// </summary>
        /// <value>
        /// The name of the container.
        /// </value>
        public string ContainerName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the end point suffix.
        /// </summary>
        /// <value>
        /// The end point suffix.
        /// </value>
        public string? EndPointSuffix { get; set; }

        
        /// <summary>
        /// Validates this object.
        /// </summary>
        /// <exception cref="AzureBlobStorageValidationException"> Thrown when an Azure BLOB Storage
        /// Validation error condition occurs.</exception>
        public void Validate()
        {
            Dictionary<string, string> validationErrors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(AccountName))
            {
                validationErrors.Add($"{nameof(AzureBlobStorageOptions)}.{nameof(AccountName)}", $"{nameof(AzureBlobStorageOptions)}.{nameof(AccountName)} must be specified");
            }
            if (string.IsNullOrEmpty(ContainerName))
            {
                validationErrors.Add($"{nameof(AzureBlobStorageOptions)}.{nameof(ContainerName)}", $"{nameof(AzureBlobStorageOptions)}.{nameof(ContainerName)} must be specified");
            }

            if (AuthMode == AzureBlobStorageAuthMode.AccountKey && string.IsNullOrEmpty(AccountKey))
            {
                validationErrors.Add($"{nameof(AzureBlobStorageOptions)}.{nameof(AccountKey)}", $"{nameof(AzureBlobStorageOptions)}.{nameof(AccountKey)} must be specified when {nameof(AuthMode)} is set to {AzureBlobStorageAuthMode.AccountKey}.");
            }

            if (!string.IsNullOrEmpty(EndPointSuffix))
            {
                var urlStr = $"https://{AccountName}.{EndPointSuffix}";
                if (!Uri.TryCreate(urlStr, UriKind.Absolute, out var _))
                {
                    validationErrors.Add($"{nameof(AzureBlobStorageOptions)}.{nameof(EndPointSuffix)}", $"{nameof(AzureBlobStorageOptions)}.{nameof(EndPointSuffix)} is not a valid suffix. The URI {urlStr} is not a valid absolute url.");
                }
            }

            if (validationErrors.Count > 0)
            {
                throw new AzureBlobStorageValidationException(validationErrors);
            }
        }
    }
}