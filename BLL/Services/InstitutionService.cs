using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class InstitutionService : IInstitutionService
    {
        private readonly IRepository<DAL.Domain.Institution> _institutionRepository;
        private readonly IRepository<DAL.Domain.Menu> _menuRepository;
        private readonly IMapper _mapper;

        public InstitutionService(IRepository<DAL.Domain.Institution> institutionRepository, IRepository<DAL.Domain.Menu> menuRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public List<Institution> GetInstitutions()
        {
            var institutions = _institutionRepository.GetAll().ToList();

            return _mapper.Map<List<Institution>>(institutions);
        }

        public async Task<Institution> GetInstitutionByMenuIdAsync(int menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
                return null;

            var institutions = _institutionRepository.GetAll()
                .FirstOrDefault(institution => institution.MenuId == menu.Id);

            return _mapper.Map<Institution>(institutions);
        }

        public async Task CreateInstitutionAsync(Institution institution)
        {
            var domain = _mapper.Map<DAL.Domain.Institution>(institution);

            await _institutionRepository.CreateAsync(domain);
        }

        public async Task UpdateInstitutionAsync(Institution institution)
        {
            var domain = _mapper.Map<DAL.Domain.Institution>(institution);

            await _institutionRepository.UpdateAsync(domain);
        }

        public async Task DeleteInstitutionAsync(Institution institution)
        {
            var domain = _mapper.Map<DAL.Domain.Institution>(institution);

            await _institutionRepository.DeleteAsync(domain.Id);
        }

        public List<Institution> FindByName(string name)
        {
            var institutions = _institutionRepository.GetAll()
                .Where(institution => institution.Name.ToLower().Contains(name.ToLower()))
                .ToList();

            return _mapper.Map<List<Institution>>(institutions);
        }
    }
}
