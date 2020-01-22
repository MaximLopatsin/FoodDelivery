using System;

namespace BLL.Exceptions
{
    [Serializable]
    public class UserCreateException : Exception
    {
        public UserCreateException()
        {
        }

        public UserCreateException(string message)
            : base(message)
        {
        }

        public UserCreateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
