using ImageUploader.Shared.Models.Commands.Images;

namespace ImageUploader.Api.Commands.Images.Upload;

public interface IUploadImageHandler
{
    Task<string> Execute(ImageUploadRequest uploadRequest);
}