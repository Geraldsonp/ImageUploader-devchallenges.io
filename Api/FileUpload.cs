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
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = null)] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        string containerName = Environment.GetEnvironmentVariable("ContainerName");
        var blobClient = new BlobContainerClient(connection, containerName);

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonConvert.DeserializeObject<ImageUploadRequest>(body);

        var blob = blobClient.GetBlobClient(request.ImgName);

        using var ms = new MemoryStream(request.ImageBytes);

        await blob.UploadAsync(ms);

        if (blob.Exists())
        {
            var uri = blob.Uri.AbsoluteUri;
            return new OkObjectResult(uri);
        }
        else
        {
            return new InternalServerErrorResult();
        }

    }
}