using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accessor.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace Accessor.DataModel
    {
    public class ChallengeTableEntity : TableEntity
        {
        [IgnoreProperty]
        public Guid Id
            {
            get { return Guid.Parse(PartitionKey);}
            set { PartitionKey = value.ToString(); }
            }

        [IgnoreProperty]
        public Guid AuthorId
            {
            get { return Guid.Parse(RowKey); }
            set { RowKey = value.ToString(); }
            }

        public string Name { get; set; }
        public Rating Rating { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string ContentUri { get; set; }
        }
    }