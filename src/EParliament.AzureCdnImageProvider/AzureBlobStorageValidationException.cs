// file:	EParliament.AzureCdnImageProvider\AzureBlobStorageValidationException.cs
//
// summary:	Implements the azure BLOB storage validation exception class

namespace EParliament.AzureCdnImageProvider
{
    /// <summary>
    /// Exception for signalling azure BLOB storage validation errors.
    /// </summary>
    public class AzureBlobStorageValidationException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errors"> The errors.</param>
        public AzureBlobStorageValidationException(IEnumerable<KeyValuePair<string, string>> errors)
            : base($"There was one or more errors while validating the {nameof(AzureBlobStorageOptions)}. Please the {nameof(ValidationErrors)} property.")
        {
            ValidationErrors = errors;
        }

        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public IEnumerable<KeyValuePair<string, string>> ValidationErrors { get; set; }
    }
}