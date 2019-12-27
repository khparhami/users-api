using System;
using MongoDB.Bson.Serialization.Attributes;

namespace zip.api.Entities
{
    public class Account
    {
        [BsonId]
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
