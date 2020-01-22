using System.Collections.Generic;

namespace BLL.Dto
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
