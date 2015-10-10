using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Accessor.Models
    {
    public class Challenge
        {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string ContentUri { get; set; }
        }
    }