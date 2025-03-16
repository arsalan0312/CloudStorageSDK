using CloudStorageSDK.Enums;
using CloudStorageSDK.Interfaces;
using CloudStorageSDK.Models;
using CloudStorageSDK.Services;

namespace CloudStorageSDK
{
    public class CloudStorageFactory
    {
        public static ICloudStorageService CreateStorageService(CloudStorageOptions options)
        {
            return options.Provider switch
            {
                CloudProvider.AWS => new AWSStorageService(options.AwsAccessKey, options.AwsSecretKey, options.BucketName, options.Region),
                CloudProvider.Azure => new AzureBlobStorageService(options.AzureConnectionString, options.BucketName),
                CloudProvider.Google => new GoogleCloudStorageService(options.BucketName, options.CredentialsFilePath),
                _ => throw new ArgumentException("Invalid cloud provider specified.")
            };
        }
    }

}
