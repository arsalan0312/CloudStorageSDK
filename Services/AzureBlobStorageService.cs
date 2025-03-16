using Azure.Storage.Blobs;
using CloudStorageSDK.Interfaces;

public class AzureBlobStorageService : ICloudStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _bucketName;

    public AzureBlobStorageService(string connectionString, string bucketName)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
        _bucketName = bucketName;
    }

    public async Task<string> UploadFileAsync(string containerName, string filePath, string destinationBlobName)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(destinationBlobName);
            var response = await blobClient.UploadAsync(filePath, overwrite: true);
            return $"File uploaded successfully to Azure Blob Storage: {response}";
        }
        catch (Exception ex)
        {
            return $"Error uploading file to Azure Blob Storage: {ex.Message}";
        }
    }

    public async Task<string> DownloadFileAsync(string containerName, string fileName)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var downloadResponse = await blobClient.DownloadAsync();
            using (var memoryStream = new MemoryStream())
            {
                await downloadResponse.Value.Content.CopyToAsync(memoryStream);
                var filePath = Path.Combine("downloads", fileName);
                await File.WriteAllBytesAsync(filePath, memoryStream.ToArray());
                return $"File downloaded successfully from Azure Blob Storage to {filePath}.";
            }
        }
        catch (Exception ex)
        {
            return $"Error downloading file from Azure Blob Storage: {ex.Message}";
        }
    }

    public async Task<string> DeleteFileAsync(string containerName, string fileName)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var response = await blobClient.DeleteIfExistsAsync();
            return $"File deleted successfully from Azure Blob Storage. {response}";
        }
        catch (Exception ex)
        {
            return $"Error deleting file from Azure Blob Storage: {ex.Message}";
        }
    }

    public async Task<string> GetFileMetadataAsync(string containerName, string fileName)
    {
        try
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var blobProperties = await blobClient.GetPropertiesAsync();
            return $"File metadata retrieved from Azure Blob Storage : {blobProperties}";
        }
        catch (Exception ex)
        {
            return $"Error getting file metadata from Azure Blob Storage: {ex.Message}";
        }
    }
}
