using Azure.Storage.Blobs;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ImageUploader.Client;
using ImageUploader.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddBlazoredToast();
builder.Services.AddSingleton(x =>
    new BlobServiceClient(
        "DefaultEndpointsProtocol=https;AccountName=gpblobfreestorage;AccountKey=B7nhwikLT4kwPAENLxxzCXa0ZhPEqGY+AlDfeVCNglDdLc2Wv/Wh8favNu41+P+SJiAHr1fvPX/r+AStrgL5yA==;EndpointSuffix=core.windows.net"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();