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
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Accessor.Models
    {
    public class UploadChallengeController : ApiController
    {
        public  void Post()
        {
            string json = Request.Content.ReadAsStringAsync().Result;
            Challenge challenge = DeserializeChallenges(json); 
            //do smth with this challenge
        }

        private Challenge DeserializeChallenges(string json)
        {
            return JsonConvert.DeserializeObject<Challenge>(json);
        }
    }
}