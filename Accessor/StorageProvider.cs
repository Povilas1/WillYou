using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace Accessor
    {
    public class StorageProvider
        {
        public static Lazy<CloudStorageAccount> StorageAccount = new Lazy<CloudStorageAccount> (() =>
            {
            var connectionString = ConfigurationManager.AppSettings["Storage.ConnectionString"];
            return CloudStorageAccount.Parse (connectionString);
            });

        public static CloudTableClient GetTableClient()
            {
            return StorageAccount.Value.CreateCloudTableClient();
            }

        public static CloudBlobClient GetBlobClient()
            {
            return StorageAccount.Value.CreateCloudBlobClient();
            }

        public static CloudQueueClient GetQueueClient()
            {
            return StorageAccount.Value.CreateCloudQueueClient();
            }
        }
    }