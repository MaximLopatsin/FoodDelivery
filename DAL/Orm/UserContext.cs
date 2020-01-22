using System.Data.Entity;
using DAL.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Orm
{
    public class UserContext : IdentityDbContext<ApplicationUser>
    {
        public UserContext(string conectionString)
            : base(conectionString)
        {
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
