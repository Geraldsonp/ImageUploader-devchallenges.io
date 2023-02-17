using ImageUploader.Api.Services;
using ImageUploader.Shared.Models.Commands.Images;

namespace ImageUploader.Api.Commands.Images.Upload;

public class UploadImageHandler : IUploadImageHandler
{
    private readonly IFileCloudManager _fileManager;

    public UploadImageHandler(IFileCloudManager fileManager)
    {
        _fileManager = fileManager;
    }

    public async Task<string> Execute(ImageUploadRequest uploadRequest)
    {
        var stream = Convert.FromBase64String(uploadRequest.base64Img);

        using var memoryStream = new MemoryStream(stream);

        var result = await _fileManager.Upload(memoryStream, uploadRequest.ImgName);

        return result;
    }
}