using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ImageUploader.Api.Common;

namespace ImageUploader.Client.Services;

public class BlobManager : IBlobManager
{
    private readonly BlobServiceClient _blobClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobManager(BlobServiceClient blobClient)
    {
        _blobClient = blobClient;
        _blobContainerClient = _blobClient.GetBlobContainerClient("test");
    }

    public async Task Upload(MemoryStream file, string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        file.Position = 0;
        var response = await blobClient.UploadAsync(file, new BlobHttpHeaders { ContentType = fileName.GetContentType() });
    }

    public Task<Uri> GetBlobAsync(string name)
    {
        var blobClient = _blobContainerClient.GetBlobClient(name);
        return Task.FromResult(blobClient.Uri);
    }
}