using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Accessor.Models
    {
    public class ChallengesController : ApiController
        {
        public HttpResponseMessage Get()
            {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse (
                ConfigurationManager.AppSettings["Storage.ConnectionString"]);

            var json = JsonConvert.SerializeObject (GetChallenges (storageAccount));

            return new HttpResponseMessage ()
                {
                Content = new StringContent (json)
                };
            }

        private List<Challenge> GetChallenges(CloudStorageAccount storageAccount)
            {
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient ();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference ("challenges");

            container.SetPermissions(
                new BlobContainerPermissions { PublicAccess =
                BlobContainerPublicAccessType.Blob });

            var blob = container.GetBlockBlobReference("wallhaven-110001.jpg");
            var uri= blob.Uri;

            List<Challenge> challenges = new List<Challenge>
                {
                new Challenge
                    {
                    Id = Guid.NewGuid(),
                    AuthorId = Guid.NewGuid(),
                    Name = "belekoks challenge",
                    Description = "visi darykit",
                    ContentType = "Picture",

                    ContentUri = uri.ToString(),
                    },
                new Challenge
                    {
                    Id = Guid.NewGuid(),
                    AuthorId = Guid.NewGuid(),
                    Name = "belekoks challenge",
                    Description = "visi darykit",
                    ContentType = "Picture",
                    ContentUri = uri.ToString(),
                    },
                };
            return challenges;
            }
        }
    }