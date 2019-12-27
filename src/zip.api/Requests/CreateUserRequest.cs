using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zip.api.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "MonthlySalary must be a positive number")]
        public decimal MonthlySalary { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "MonthlyExpenses must be a positive number")]
        public decimal MonthlyExpenses { get; set; }
    }
}
