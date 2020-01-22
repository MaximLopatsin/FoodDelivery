using System;

namespace BLL.Exceptions
{
    [Serializable]
    public class AuthenticateException : Exception
    {
        public AuthenticateException()
        {
        }

        public AuthenticateException(string message)
            : base(message)
        {
        }

        public AuthenticateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}