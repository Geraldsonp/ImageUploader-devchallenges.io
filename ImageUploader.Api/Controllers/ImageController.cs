using ImageUploader.Api.Commands.Images.Upload;
using ImageUploader.Shared.Models.Commands.Images;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploader.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ImageController : ControllerBase
{
    private readonly IUploadImageHandler _uploadHandler;

    public ImageController(IUploadImageHandler uploadHandler)
    {
        _uploadHandler = uploadHandler;
    }


    [HttpPost]
    public async Task<ActionResult<string>> Upload(ImageUploadRequest uploadRequest)
    {
        var result = await _uploadHandler.Execute(uploadRequest);

        return Ok(result);
    }
}