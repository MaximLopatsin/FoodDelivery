using DAL.Domain;

namespace DAL.Interfaces
{
    public interface IClientRepository
    {
        void Create(ClientProfile item);

        void ChangeName(string id, string newName);

        ClientProfile GetClient(string id);
    }
}
