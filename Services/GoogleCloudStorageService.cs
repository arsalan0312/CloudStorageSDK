using CloudStorageSDK.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace CloudStorageSDK.Services
{
    public class GoogleCloudStorageService : ICloudStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorageService(string bucketName, string credentialsFilePath)
        {
            GoogleCredential credentials = GoogleCredential.FromFile(credentialsFilePath);

            // Create a StorageClient using the credentials
            _storageClient = StorageClient.Create(credentials);
            _bucketName = bucketName;
        }

        public async Task<string> UploadFileAsync(string containerName, string filePath, string destinationBlobName)
        {
            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var storageObject = await _storageClient.UploadObjectAsync(containerName, destinationBlobName, null, fileStream);
                    return $"File uploaded successfully to Google Cloud Storage: {storageObject}";
                }
            }
            catch (Exception ex)
            {
                return $"Error uploading file to Google Cloud Storage: {ex.Message}";
            }
        }

        public async Task<string> DownloadFileAsync(string containerName, string fileName)
        {
            try
            {
                var memoryStream = new MemoryStream();

                await _storageClient.DownloadObjectAsync(containerName, fileName, memoryStream);

                var filePath = Path.Combine("downloads", fileName);
                await File.WriteAllBytesAsync(filePath, memoryStream.ToArray());

                return $"File downloaded successfully from Google Cloud Storage to {filePath}.";
            }
            catch (Exception ex)
            {
                return $"Error downloading file from Google Cloud Storage: {ex.Message}";
            }
        }

        public async Task<string> DeleteFileAsync(string containerName, string fileName)
        {
            try
            {
                await _storageClient.DeleteObjectAsync(containerName, fileName);
                return $"File deleted successfully from Google Cloud Storage.";
            }
            catch (Exception ex)
            {
                return $"Error deleting file from Google Cloud Storage: {ex.Message}";
            }
        }

        public async Task<string> GetFileMetadataAsync(string containerName, string fileName)
        {
            try
            {
                var storageObject = await _storageClient.GetObjectAsync(containerName, fileName);
                return $"File metadata retrieved from Google Cloud Storage : {storageObject}";
            }
            catch (Exception ex)
            {
                return $"Error getting file metadata from Google Cloud Storage: {ex.Message}";
            }
        }
    }

}
