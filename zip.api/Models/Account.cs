using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zip.api.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
    }
}
