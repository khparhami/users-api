using System;

namespace zip.api.Exceptions
{
    public class UserAlreadyExistsException: Exception
    {
        public UserAlreadyExistsException() : base("User already exist with provided email address")
        {

        }
    }
}
