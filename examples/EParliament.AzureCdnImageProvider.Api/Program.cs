using EParliament.AzureCdnImageProvider.AspNet;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>(true);
}


builder.Services.ConfigureEParliamentAzureCdnMiddleware(o =>
{
    o.WithPathPrefix("/api/images")
     .WithAzureBlobStorageOptions(builder.Configuration, "AzureBlobStorage");

});


var app = builder.Build();
app.UseEParliamentAzureCdnMiddleware();
app.Run();
