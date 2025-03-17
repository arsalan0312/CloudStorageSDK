# CloudStorageSDK

CloudStorageSDK is a unified service that simplifies interactions with Azure Blob Storage, AWS S3, and Google Cloud Storage. It provides a consistent interface for uploading, downloading, and managing files across these cloud platforms.

## Features

- **Unified Interface:** Seamlessly interact with Azure Blob Storage, AWS S3, and Google Cloud Storage using a consistent API.
- **File Operations:** Easily upload, download, and delete files across multiple cloud platforms.
- **Cross-Platform Support:** Compatible with .NET 6.0 and above.

## Installation

To install CloudStorageSDK, use the NuGet Package Manager or the .NET CLI.

**Using NuGet Package Manager:**

1. Open the NuGet Package Manager in Visual Studio.
2. Search for `CloudStorageSDK`.
3. Click **Install**.

**Using .NET CLI:**

```bash
dotnet add package CloudStorageSDK


Configuration
Before using the SDK, configure your cloud storage settings.

Azure Blob Storage:

Connection String: Obtain your connection string from the Azure portal.
Container Name: Specify the name of your Azure Blob Storage container.
AWS S3:

Access Key ID: Your AWS access key ID.
Secret Access Key: Your AWS secret access key.
Bucket Name: The name of your S3 bucket.
Google Cloud Storage:

Project ID: Your Google Cloud project ID.
Bucket Name: The name of your Google Cloud Storage bucket.
Credentials File: Path to your service account key file.

var storageService = new CloudStorageService(
    azureConnectionString: "YourAzureConnectionString",
    awsAccessKeyId: "YourAWSAccessKeyId",
    awsSecretAccessKey: "YourAWSSecretAccessKey",
    googleProjectId: "YourGoogleProjectId",
    googleCredentialsFile: "PathToYourGoogleCredentialsFile"
);


// Upload to Azure Blob Storage
await storageService.UploadFileAsync("azure-container-name", "local-file-path", "azure-blob-name");

// Upload to AWS S3
await storageService.UploadFileAsync("aws-bucket-name", "local-file-path", "aws-object-key");

// Upload to Google Cloud Storage
await storageService.UploadFileAsync("google-bucket-name", "local-file-path", "google-object-name");


// Download from Azure Blob Storage
await storageService.DownloadFileAsync("azure-container-name", "azure-blob-name", "local-file-path");

// Download from AWS S3
await storageService.DownloadFileAsync("aws-bucket-name", "aws-object-key", "local-file-path");

// Download from Google Cloud Storage
await storageService.DownloadFileAsync("google-bucket-name", "google-object-name", "local-file-path");


// Delete from Azure Blob Storage
await storageService.DeleteFileAsync("azure-container-name", "azure-blob-name");

// Delete from AWS S3
await storageService.DeleteFileAsync("aws-bucket-name", "aws-object-key");

// Delete from Google Cloud Storage
await storageService.DeleteFileAsync("google-bucket-name", "google-object-name");

