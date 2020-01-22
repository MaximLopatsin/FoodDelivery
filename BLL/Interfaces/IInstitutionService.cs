using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IInstitutionService
    {
        List<Institution> GetInstitutions();

        Task<Institution> GetInstitutionByMenuIdAsync(int menuId);

        List<Institution> FindByName(string name);

        Task CreateInstitutionAsync(Institution institution);

        Task UpdateInstitutionAsync(Institution institution);

        Task DeleteInstitutionAsync(Institution institution);
    }
}
