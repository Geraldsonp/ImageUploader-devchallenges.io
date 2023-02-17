using Azure.Storage.Blobs.Models;

namespace ImageUploader.Client.Services;

public interface IBlobManager
{
    Task Upload(MemoryStream file, string fileName);
    Task<Uri> GetBlobAsync(string name);
}