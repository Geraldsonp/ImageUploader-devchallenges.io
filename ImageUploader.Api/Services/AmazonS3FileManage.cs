using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace ImageUploader.Api.Services;

public class AmazonS3FileManage : IFileCloudManager
{
    private const string BucketName = "imageuploadertestbucket";
    private readonly TransferUtility _transferUtility;

    public AmazonS3FileManage()
    {
        var credentials =
            new BasicAWSCredentials(Environment.GetEnvironmentVariable("TestS3AccessKey"), Environment.GetEnvironmentVariable("TestS3SecretKey"));
        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast1
        };

        var client = new AmazonS3Client(credentials, config);

        _transferUtility = new TransferUtility(client);
    }

    public async Task<string> Upload(MemoryStream stream, string name)
    {
        var uploadRequest = new TransferUtilityUploadRequest()
        {
            InputStream = stream,
            Key = name,
            BucketName = BucketName,
            CannedACL = S3CannedACL.NoACL
        };

        await _transferUtility.UploadAsync(uploadRequest);

        return $"https://imageuploadertestbucket.s3.amazonaws.com/{name}";

    }
}