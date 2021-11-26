// file:	EParliament.AzureCdnImageProvider.AspNet\EParliamentMiddleware.cs
//
// summary:	Implements the parliament middleware class
using Imageflow.Fluent;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EParliament.AzureCdnImageProvider.AspNet
{
    /// <summary>
    /// A parliament middleware.
    /// </summary>
    public class EParliamentMiddleware
    {
        /// <summary>
        /// (Immutable) the next.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// (Immutable) options for controlling the operation.
        /// </summary>
        private readonly EParliamentMiddlewareOptions _options;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next"> The next.</param>
        /// <param name="options"> Options for controlling the operation.</param>
        public EParliamentMiddleware(RequestDelegate next, EParliamentMiddlewareOptions options)
        {
            _next = next;
            _options = options;
        }

        /// <summary>
        /// Executes the asynchronous on a different thread, and waits for the result.
        /// </summary>
        /// <param name="context"> The context.</param>
        /// <param name="provider"> The provider.</param>
        /// <param name="token"> A token that allows processing to be cancelled.</param>
        /// <returns>
        /// A Task.
        /// </returns>
        public async Task InvokeAsync(HttpContext context, IServiceProvider provider)
        {
            if (context.Request.Path.StartsWithSegments(_options.PathPrefix))
            {
                var token = context.RequestAborted;
                //var  blobService  = provider.Get
                await HandleImagePathAsync(context, provider, token);

                return;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        /// <summary>
        /// Handles the image path asynchronous.
        /// </summary>
        /// <param name="context"> The context.</param>
        /// <param name="provider"> The provider.</param>
        /// <param name="token"> A token that allows processing to be cancelled.</param>
        /// <returns>
        /// A Task.
        /// </returns>
        private async Task HandleImagePathAsync(HttpContext context, IServiceProvider provider, CancellationToken token)
        {
            var blobService = provider.GetRequiredService<IBlobClientService>();
            var path = context.Request.Path.ToString().ToLowerInvariant().Replace(_options.PathPrefix, "").TrimStart('/');

            var containerClient = blobService.ContainerClient;

            var blobClient = containerClient.GetBlobClient(path);
            if (!(await blobClient.ExistsAsync()))
            {
                context.Response.StatusCode = 404;
                return;
            }

            var content = await blobClient.DownloadContentAsync();
            var bytes = content.Value.Content.ToArray();

            if (!string.IsNullOrEmpty(context.Request.QueryString.ToString()))
            {
                var qs = context.Request.QueryString.ToString().TrimStart('?');

                using var b = new ImageJob();
                var result = await b.BuildCommandString(bytes, new BytesDestination(), qs)
                                    .Finish()
                                    .WithCancellationToken(token)
                                    .InProcessAsync();

                var resultSegment = result.First.TryGetBytes();
                if (!resultSegment.HasValue)
                {
                    context.Response.StatusCode = 404;
                    return;
                }
                
                var resultBytes = resultSegment.Value.ToArray();

                await WriteResponse(context, resultBytes, result.First.PreferredMimeType, token);
            }
            else
            {
                await WriteResponse(context, bytes, content.Value.Details.ContentType, token);
            }
        }

        /// <summary>
        /// Writes a response.
        /// </summary>
        /// <param name="context"> The context.</param>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="contentType"> Type of the content.</param>
        /// <param name="token"> A token that allows processing to be cancelled.</param>
        /// <returns>
        /// A Task.
        /// </returns>
        private async Task WriteResponse(HttpContext context, ReadOnlyMemory<byte> bytes, string contentType, CancellationToken token)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentLength = bytes.Length;
            context.Response.ContentType = contentType;
            await context.Response.BodyWriter.WriteAsync(bytes, token);
        }
    }
}