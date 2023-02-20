using System.Net.Http.Json;
using ImageUploader.Shared.Models;
using ImageUploader.Shared.Models.Commands.Images;
using Microsoft.AspNetCore.Components.Forms;

namespace ImageUploader.Client.Services;

public class ImageService : IImageService
{
    public string ApiAddress { get; set; } = "/api/Upload";
    private readonly HttpClient _httpClient;

    public ImageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UploadResult> UploadToBlobAsync(ImageUploadRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiAddress, request);

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

    public async Task<UploadResult> UploadAsync(ImageUploadRequest request)
    {

        var response = await _httpClient.PostAsJsonAsync(ApiAddress, request);

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