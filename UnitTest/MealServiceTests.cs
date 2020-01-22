using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.MapperProfile;
using BLL.Services;
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
        public void GetIngredientsByMealIdAsync_WhenIdNotExist_ShouldReturnData()
        {
            // Arrange
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
