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
using System.Threading.Tasks;

namespace Accessor.Controllers
    {
    public class ChallengesController : ApiController
        {
        public HttpResponseMessage Get()
            {
            var json = JsonConvert.SerializeObject (GetAllChallenges ());

            return new HttpResponseMessage ()
                {
                Content = new StringContent (json)
                };
            }

        public HttpResponseMessage Get(int id)
            {
            var json = JsonConvert.SerializeObject (GetAllChallenges ());

            return new HttpResponseMessage ()
            {
                Content = new StringContent (json)
            };
            }

        public async Task Post()
            {
            string json = Request.Content.ReadAsStringAsync ().Result;
            Challenge challenge = DeserializeChallenges (json);
            ChallengeTableEntity entity = new ChallengeTableEntity
            {
                Id = challenge.Id,
                AuthorId = challenge.AuthorId,
                Name = challenge.Name,
                Rating = challenge.Rating,
                Description = challenge.Description,
                ContentType = challenge.ContentType,
                ContentUri = challenge.ContentUri,
                ParentId = challenge.ParentId,
            };

            var tableClient = StorageProvider.GetTableClient();
            var table = tableClient.GetTableReference("challenges");

            table.CreateIfNotExists();
            await table.ExecuteAsync(TableOperation.Insert(entity));
            }

        private Challenge DeserializeChallenges(string json)
            {
            return JsonConvert.DeserializeObject<Challenge> (json);
            }

        private List<Challenge> GetAllChallenges()
            {
            // Create the blob client.
            var blobClient = StorageProvider.GetBlobClient();
            var tableClient = StorageProvider.GetTableClient();

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
                ParentId = x.ParentId,
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