using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.MapperProfile;
using BLL.Services;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class InstitutionServiceTests
    {
        private Mock<IRepository<Institution>> _institutionRepositoryMock;
        private Mock<IRepository<Menu>> _menuRepositoryMock;
        private IMapper _mapper;

        public InstitutionServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BLLMapperProfile());
            });

            _mapper = config.CreateMapper();
        }

        [SetUp]
        public void CreateMocks()
        {
            _institutionRepositoryMock = new Mock<IRepository<Institution>>();
            _menuRepositoryMock = new Mock<IRepository<Menu>>();
        }

        [Test]
        public void GetInstitutions_WhenDataExist_ShouldReturnData()
        {
            // Arrange
            _institutionRepositoryMock.Setup(a => a.GetAll())
                .Returns(new List<Institution>()
                {
                    new Institution { Id = 1 },
                }.AsQueryable());

            var service = GetService();

            // Act
            var items = service.GetInstitutions();

            // Assert
            Assert.That(items.Any());
        }

        [Test]
        public void GetInstitutions_WhenDataNotExist_ShouldReturnEmptyList()
        {
            // Arrange
            var service = GetService();

            // Act
            var items = service.GetInstitutions();

            // Assert
            Assert.That(!items.Any());
        }

        [Test]
        public void GetInstitutionByMenuIdAsync_WhenIdNotExist_ShouldReturnNull()
        {
            // Arrange
            _institutionRepositoryMock.Setup(a => a.GetAll())
                .Returns(new List<Institution>()
                {
                    new Institution { Id = 1, MenuId = 1 },
                }.AsQueryable());
            _menuRepositoryMock.Setup(a => a.GetByIdAsync(It.IsAny<int>())).Returns((int id) =>
            {
                if (id == 1)
                    return Task.FromResult(new Menu { Id = 1 });

                return null;
            });

            var service = GetService();

            // Act
            var item = Task.Run(() => service.GetInstitutionByMenuIdAsync(10)).Result;

            // Assert
            Assert.IsNull(item);
        }

        [Test]
        public void GetInstitutionByMenuIdAsync_WhenIdExist_ShouldReturnData()
        {
            // Arrange
            _institutionRepositoryMock.Setup(a => a.GetAll())
                .Returns(new List<Institution>()
                {
                    new Institution { Id = 1, MenuId = 1 },
                }.AsQueryable());
            _menuRepositoryMock.Setup(a => a.GetByIdAsync(It.IsAny<int>())).Returns((int id) =>
            {
                if (id == 1)
                    return Task.FromResult(new Menu { Id = 1 });

                return null;
            });

            var service = GetService();

            // Act
            var item = Task.Run(() => service.GetInstitutionByMenuIdAsync(1)).Result;

            // Assert
            Assert.IsNotNull(item);
        }

        [Test]
        public void FindByName_WhenNameExist_ShouldReturnData()
        {
            // Arrange
            string name = "NameTest";
            _institutionRepositoryMock.Setup(a => a.GetAll())
                .Returns(new List<Institution>()
                {
                    new Institution { Name = "nametest" },
                }.AsQueryable());

            var service = GetService();

            // Act
            var items = service.FindByName(name);

            // Assert
            Assert.IsNotEmpty(items);
        }

        [Test]
        public void FindByName_WhenNameNotExist_ShouldReturnData()
        {
            // Arrange
            string name = "NameTest";
            _institutionRepositoryMock.Setup(a => a.GetAll())
                .Returns(new List<Institution>()
                {
                    new Institution { Name = "test" },
                }.AsQueryable());

            var service = GetService();

            // Act
            var items = service.FindByName(name);

            // Assert
            Assert.IsEmpty(items);
        }

        private IInstitutionService GetService()
        {
            return new InstitutionService(
                _institutionRepositoryMock.Object,
                _menuRepositoryMock.Object,
                _mapper);
        }
    }
}
