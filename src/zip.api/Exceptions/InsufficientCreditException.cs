using System;

namespace zip.api.Exceptions
{
    public class InsufficientCreditException: Exception
    {
        public InsufficientCreditException():base("User does not have sufficient credit")
        {

        }
    }
}
