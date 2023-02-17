using Microsoft.AspNetCore.StaticFiles;

namespace ImageUploader.Api.Common;

public static class FileExtension
{
    private static readonly FileExtensionContentTypeProvider _provider = new FileExtensionContentTypeProvider();

    public static string GetContentType(this string filename)
    {
        if (!_provider.TryGetContentType(filename, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }
}