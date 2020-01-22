using System;

namespace BLL.Exceptions
{
    [Serializable]
    public class UserNotExistException : Exception
    {
        public UserNotExistException()
        {
        }

        public UserNotExistException(string message)
            : base(message)
        {
        }

        public UserNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}