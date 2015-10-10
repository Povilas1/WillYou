using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace Accessor.Controllers
    {
    public class RepositoryController : ApiController
        {
        public HttpResponseMessage Get()
            {
            var blobClient = StorageProvider.GetBlobClient();
            var container = blobClient.GetContainerReference("challenges");

            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            var uri = blob.Uri.ToString();

            return new HttpResponseMessage ()
                {
                Content = new StringContent (uri)
                };
            }

        public HttpResponseMessage Get(string id)
            {
            var blobClient = StorageProvider.GetBlobClient ();
            var container = blobClient.GetContainerReference ("challenges");

            var blob = container.GetBlockBlobReference (Guid.NewGuid ().ToString ());
            var uri = blob.Uri.ToString ();

            return new HttpResponseMessage ()
                {
                Content = new StringContent (uri)
                };
            }

        }
    }