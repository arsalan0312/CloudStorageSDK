using CloudStorageSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorageSDK.Models
{
    public class CloudStorageOptions
    {
        public CloudProvider Provider { get; set; }

        // AWS S3 Configurations
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string BucketName { get; set; }
        public string Region { get; set; }

        // Azure Blob Storage Configurations
        public string AzureConnectionString { get; set; }

        // Google Cloud Storage Configurations
        public string ProjectId { get; set; }
        public string CredentialsFilePath { get; set; }
    }
}
