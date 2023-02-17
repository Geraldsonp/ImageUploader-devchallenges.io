using System.ComponentModel.DataAnnotations;

namespace ImageUploader.Shared.Models.Commands.Images;

public class ImageUploadRequest
{
    [DataType(DataType.Upload)]
    [Required]
    public string base64Img { get; set; }
    [Required]
    public string ImgName { get; set; }
}