using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zip.api.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public float MonthlySalary { get; set; }

        public float MonthlyExpenses { get; set; }
    }
}
