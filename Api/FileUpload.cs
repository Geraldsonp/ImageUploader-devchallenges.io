using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Web.Http;
using Azure.Storage.Blobs;
using ImageUploader.Shared.Models.Commands.Images;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FileUploadFunction;

public static class FileUpload
{
    [FunctionName("FileUpload")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
        HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        string containerName = Environment.GetEnvironmentVariable("ContainerName");
        var blobContainer = new BlobContainerClient(connection, containerName);

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var imageUploadRequest = JsonConvert.DeserializeObject<ImageUploadRequest>(body);

        var blobClient = blobContainer.GetBlobClient(imageUploadRequest.ImgName);

        using var ms = new MemoryStream(imageUploadRequest.ImageBytes);

        if (!blobClient.Exists())
        {
            await blobClient.UploadAsync(ms);
            log.LogInformation($"Uploaded Blob {imageUploadRequest.ImgName}");
            var uri = blobClient.Uri.AbsoluteUri;
            return new OkObjectResult(uri);
        }
        else
        {
            log.LogInformation($"Blob {imageUploadRequest.ImgName} already exist, Returning URI");
            var uri = blobClient.Uri.AbsoluteUri;
            return new OkObjectResult(uri);
        }

        return new InternalServerErrorResult();
    }
}