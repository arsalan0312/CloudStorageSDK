namespace CloudStorageSDK.Interfaces
{
    public interface ICloudStorageService
    {
        Task<string> UploadFileAsync(string containerName, string filePath, string destinationBlobName);
        Task<string> DownloadFileAsync(string containerName, string fileName);
        Task<string> DeleteFileAsync(string containerName, string fileName);
        Task<string> GetFileMetadataAsync(string containerName, string fileName);
    }
}
