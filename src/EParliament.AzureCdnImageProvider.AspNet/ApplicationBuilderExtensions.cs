// file:	EParliament.AzureCdnImageProvider.AspNet\ApplicationBuilderExtensions.cs
//
// summary:	Implements the application builder extensions class
using Microsoft.AspNetCore.Builder;

namespace EParliament.AzureCdnImageProvider.AspNet
{
    /// <summary>
    /// An application builder extensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use parliament azure cdn middleware.
        /// </summary>
        /// <param name="builder"> The builder.</param>
        /// <returns>
        /// An IApplicationBuilder.
        /// </returns>
        public static IApplicationBuilder UseEParliamentAzureCdnMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EParliamentMiddleware>();
        }

        
    }
}