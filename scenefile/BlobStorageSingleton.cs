using System;
using System.IO;
using Azure.Storage.Blobs;

namespace scenefile
{
    class BlobStorageSingleton
    {
        public static void UploadSas(String SasUrl, Stream content)
        {
            Uri SasUri = new Uri(SasUrl);
            BlobClient blob = new BlobClient(SasUri);
            blob.Upload(content, true);
        }
    }
}