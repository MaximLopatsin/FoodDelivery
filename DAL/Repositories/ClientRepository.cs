using System.Data.Entity;
using DAL.Domain;
using DAL.Interfaces;
using DAL.Orm;

namespace DAL.Repositories
{
    internal class ClientRepository : IClientRepository
    {
        private readonly UserContext _context;

        public ClientRepository(UserContext context)
        {
            _context = context;
        }

        public void Create(ClientProfile item)
        {
            _context.ClientProfiles.Add(item);
            _context.SaveChanges();
        }

        public void ChangeName(string id, string newName)
        {
            var item = _context.ClientProfiles.Find(id);
            if (item != null)
            {
                item.Name = newName;
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public ClientProfile GetClient(string id)
        {
            return _context.ClientProfiles.Find(id);
        }
    }
}
