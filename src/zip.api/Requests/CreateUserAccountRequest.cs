using System.ComponentModel.DataAnnotations;

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
