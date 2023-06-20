using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Domain.OptionConfigurations;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;

namespace Presentation.Functions;

public sealed class TimerTriggerBlobClientBinding
{
    private readonly BlobStorageOptions _blobStorageOptions;

    public TimerTriggerBlobClientBinding(IOptions<BlobStorageOptions> blobStoreOptions)
    {
        _blobStorageOptions = blobStoreOptions.Value;
    }

    [FunctionName("TimerTriggerBlobClientBinding")]
    public static async Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        [Blob("%Storage:ContainerName%",
            FileAccess.Write,
            Connection = "Storage:ConnectionString")]
        BlobContainerClient blobContainerClient)
    {
    }

    private BlobClient GetBlobClient(BlobContainerClient blobContainerClient) =>
        blobContainerClient.GetBlobClient(
            $"{_blobStorageOptions.FolderName}/{DateTime.UtcNow:yyyy/MM/dd}.csv");
}