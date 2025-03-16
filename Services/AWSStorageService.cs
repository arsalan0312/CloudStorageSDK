using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CloudStorageSDK.Interfaces;

namespace CloudStorageSDK.Services
{
    public class AWSStorageService : ICloudStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _bucketUrl;

        public AWSStorageService(string accessKey, string secretKey, string bucketName, string region)
        {
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region)
            };
            _s3Client = new AmazonS3Client(credentials, config);
            _bucketName = bucketName;
            _bucketUrl = $"https://{_bucketName}.s3.{region}.amazonaws.com";
        }

        public async Task<string> UploadFileAsync(string containerName, string filePath, string destinationBlobName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_s3Client);
                await fileTransferUtility.UploadAsync(filePath, containerName);

                return $"File uploaded successfully to S3. URL: {_bucketUrl}/{destinationBlobName}";
            }
            catch (Exception ex)
            {
                return $"Error uploading file to S3: {ex.Message}";
            }
        }

        public async Task<string> DownloadFileAsync(string containerName, string fileName)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = containerName,
                    Key = fileName
                };

                using (var response = await _s3Client.GetObjectAsync(request))
                using (var stream = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(stream);
                    stream.Position = 0;
                    return $"File downloaded successfully from S3. Content length: {stream.Length}";
                }
            }
            catch (Exception ex)
            {
                return $"Error downloading file from S3: {ex.Message}";
            }
        }

        public async Task<string> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = containerName,
                    Key = fileName
                };

                var response = await _s3Client.DeleteObjectAsync(request);
                return $"File deleted successfully from S3. Status: {response.HttpStatusCode}";
            }
            catch (Exception ex)
            {
                return $"Error deleting file from S3: {ex.Message}";
            }
        }


        public async Task<string> GetFileMetadataAsync(string containerName, string fileName)
        {
            try
            {
                var request = new GetObjectMetadataRequest
                {
                    BucketName = containerName,
                    Key = fileName
                };

                var metadata = await _s3Client.GetObjectMetadataAsync(request);
                return $"File metadata retrieved successfully from S3. ETag: {metadata.ETag}, LastModified: {metadata.LastModified}";
            }
            catch (Exception ex)
            {
                return $"Error getting file metadata from S3: {ex.Message}";
            }
        }
    }

}
