using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zip.api.Requests
{
    public class CreateUserAccountRequest
    {
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
