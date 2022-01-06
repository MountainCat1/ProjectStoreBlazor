using System;

namespace ProjectStoreBlazor.Server.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message) : base(message)
        {

        }
    }
}
