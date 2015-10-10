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


            //var json = JsonConvert.SerializeObject ();

            return new HttpResponseMessage ()
            {
                Content = new StringContent (json)
            };
            }

        }
    }