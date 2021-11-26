// file:	EParliament.AzureCdnImageProvider.AspNet\EParliamentMiddlewareOptions.cs
//
// summary:	Implements the parliament middleware options class

namespace EParliament.AzureCdnImageProvider.AspNet
{
    /// <summary>
    /// A parliament middleware options.
    /// </summary>
    public class EParliamentMiddlewareOptions
    {
        /// <summary>
        /// Gets or sets the path prefix.
        /// </summary>
        /// <value>
        /// The path prefix.
        /// </value>
        public string PathPrefix { get; set; } = null!;
    }
}