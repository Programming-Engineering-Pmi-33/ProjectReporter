using System;

namespace ProjectReporter.Modules.UsersService.Exceptions
{
    public class UsersServiceException : Exception
    {
        public UsersServiceException()
        { }

        public UsersServiceException(string message) : base(message)
        { }
    }
}