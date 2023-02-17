namespace ImageUploader.Api.Services;

public interface IFileCloudManager
{
    Task<string> Upload(MemoryStream stream, string name);
}