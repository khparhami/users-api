using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zip.api.Requests
{
    public class CreateUserAccountRequest
    {
        public float Balance { get; set; }
        public string Currency { get; set; }
    }
}
