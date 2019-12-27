using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace zip.api.Entities
{
    public class User
    {
        [BsonId]
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public decimal MonthlySalary { get; set; }

        public decimal MonthlyExpenses { get; set; }

        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
