using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;
using Accessor.DataModel;
using Accessor.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace Accessor.Controllers
    {
    public class ChallengesController : ApiController
        {
        public HttpResponseMessage Get()
            {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse (
                ConfigurationManager.AppSettings["Storage.ConnectionString"]);

            var json = JsonConvert.SerializeObject (GetAllChallenges (storageAccount));

            return new HttpResponseMessage ()
                {
                Content = new StringContent (json)
                };
            }

        public HttpResponseMessage Get(int id)
            {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse (
                ConfigurationManager.AppSettings["Storage.ConnectionString"]);

            var json = JsonConvert.SerializeObject (GetAllChallenges (storageAccount));

            return new HttpResponseMessage ()
            {
                Content = new StringContent (json)
            };
            }

        private List<Challenge> GetAllChallenges(CloudStorageAccount storageAccount)
            {
            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient ();
            var tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference ("challenges");

            //todo: generate sas key, set permissions to private
            container.SetPermissions(
                new BlobContainerPermissions { PublicAccess =
                BlobContainerPublicAccessType.Blob });

            var table = tableClient.GetTableReference("challenges");
            table.CreateIfNotExists();

            var query = table.CreateQuery<ChallengeTableEntity>();
            var entities = table.ExecuteQuery(query);

            //var blob = container.GetBlockBlobReference("wallhaven-110001.jpg");
            //var uri= blob.Uri;

            var challenges = entities.Select(x => new Challenge
                {
                Id = x.Id,
                AuthorId = x.AuthorId,
                Name = x.Name,
                Rating = x.Rating,
                Description = x.Description,
                ContentType = x.ContentType,
                ContentUri = x.ContentUri,
                }).ToList();

            //List<Challenge> challenges = new List<Challenge>
            //    {
            //    new Challenge
            //        {
            //        Id = Guid.NewGuid(),
            //        AuthorId = Guid.NewGuid(),
            //        Name = "belekoks challenge",
            //        Description = "visi darykit",
            //        ContentType = "Picture",

            //        ContentUri = uri.ToString(),
            //        },
            //    new Challenge
            //        {
            //        Id = Guid.NewGuid(),
            //        AuthorId = Guid.NewGuid(),
            //        Name = "belekoks challenge",
            //        Description = "visi darykit",
            //        ContentType = "Picture",
            //        ContentUri = uri.ToString(),
            //        },
            //    };
            return challenges;
            }
        }
    }