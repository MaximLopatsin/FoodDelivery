using System;

namespace BLL.Exceptions
{
    [Serializable]
    public class RoleExistException : Exception
    {
        public RoleExistException()
        {
        }

        public RoleExistException(string message)
            : base(message)
        {
        }

        public RoleExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}