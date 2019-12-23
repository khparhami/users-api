using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zip.api.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public float MonthlySalary { get; set; }

        public float MonthlyExpenses { get; set; }

        public Account[] Accounts { get; set; }
    }
}
