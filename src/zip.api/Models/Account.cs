using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace zip.api.Models
{
    public class Account
    {
        [BsonId]
        public Guid AccountId { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
    }
}
