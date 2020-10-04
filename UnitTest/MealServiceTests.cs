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
    class MealServiceTests
    {
        private Mock<IRepository<DAL.Domain.Menu>> _menuRepositoryMock;
        private Mock<IRepository<DAL.Domain.Meal>> _mealRepositoryMock;
        private Mock<IRepository<DAL.Domain.MenuMeal>> _menuMealRepositoryMock;
        private Mock<IRepository<DAL.Domain.Ingredient>> _ingredientRepositoryMock;
        private Mock<IRepository<DAL.Domain.MealIngredient>> _mealIngredientRepositoryMock;
        private IMapper _mapper;

        public MealServiceTests()
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
            _menuRepositoryMock = new Mock<IRepository<DAL.Domain.Menu>>();
            _mealRepositoryMock = new Mock<IRepository<DAL.Domain.Meal>>();
            _menuMealRepositoryMock = new Mock<IRepository<DAL.Domain.MenuMeal>>();
            _ingredientRepositoryMock = new Mock<IRepository<DAL.Domain.Ingredient>>();
            _mealIngredientRepositoryMock = new Mock<IRepository<DAL.Domain.MealIngredient>>();
        }

        [Test]
        public void GetIngredientsByMealIdAsync_WhenIdNotExist_ShouldReturnEmptyList()
        {
            // Arrange
            var service = GetService();

            // Act
            var items = Task.Run(() => service.GetIngredientsByMealIdAsync(10)).Result;

            // Assert
            Assert.IsEmpty(items);
        }

        [Test]
        public void GetIngredientsByMealIdAsync_WhenIdExist_ShouldReturnData()
        {
            // Arrange
            var mealIng = new List<MealIngredient>
            {
                new MealIngredient{MealId = 1, IngredientId = 1,},
                new MealIngredient{MealId = 1, IngredientId = 2,},
                new MealIngredient{MealId = 2, IngredientId = 3,},
            };

            _mealRepositoryMock.Setup(a => a.GetByIdAsync(It.IsAny<int>()))
                .Returns((int id) => Task.FromResult(new Meal { Id = id, }));

            _mealIngredientRepositoryMock.Setup(a => a.GetAll()).Returns(mealIng.AsQueryable());
            _ingredientRepositoryMock.Setup(a => a.GetByIdAsync(It.IsAny<int>()))
                .Returns((int id) => Task.FromResult(new Ingredient { Id = id, }));

            var expectedResult = new List<Ingredient>
            {
                new Ingredient{Id = 1},
                new Ingredient{Id = 2},
            };

            var service = GetService();

            // Act
            var items = Task.Run(() => service.GetIngredientsByMealIdAsync(1)).Result;

            // Assert
            Assert.IsNotEmpty(items);
        }

        private IMealService GetService()
        {
            return new MealService(
                _menuRepositoryMock.Object,
                _mealRepositoryMock.Object,
                _menuMealRepositoryMock.Object,
                _ingredientRepositoryMock.Object,
                _mealIngredientRepositoryMock.Object,
                _mapper);
        }
    }
}
