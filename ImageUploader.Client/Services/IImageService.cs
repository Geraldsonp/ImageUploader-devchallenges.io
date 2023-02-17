using ImageUploader.Shared.Models;
using ImageUploader.Shared.Models.Commands.Images;

namespace ImageUploader.Client.Services;

public interface IImageService
{
    Task<UploadResult> UploadAsync(ImageUploadRequest request);
}