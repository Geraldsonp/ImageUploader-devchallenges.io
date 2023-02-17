using System.Net.Http.Json;
using ImageUploader.Shared.Models;
using ImageUploader.Shared.Models.Commands.Images;

namespace ImageUploader.Client.Services;

public class ImageService : IImageService
{
    private readonly HttpClient _httpClient;

    public ImageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UploadResult> UploadAsync(ImageUploadRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"http://localhost:5228/Image", request);

        var result = new UploadResult();

            result.IsSucess = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.ImgUrl = await response.Content.ReadAsStringAsync();
            }
            else
            {
                result.Error = await response.Content.ReadAsStringAsync();
            }

            return result;
    }
}