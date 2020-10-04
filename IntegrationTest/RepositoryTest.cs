using DAL.Domain;
using DAL.Interfaces;
using DAL.Orm;
using DAL.Repositories;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace IntegrationTest
{
    [TestFixture]
    public class InstitutionRepositoryTest
    {
        private DeliveryContext _context;
        private IRepository<Institution> _repository;

        public InstitutionRepositoryTest()
        {
            _context = new DeliveryContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _repository = new GenericRepository<Institution>(_context);
        }

        [OneTimeTearDown]
        public void DropDatabase()
        {
            _context.Database.Delete();
        }

        [Test]
        [Order(1)]
        public void GetAll_WhenDataCorrect_ShouldReturnFullList()
        {
            // Arrange
            var expectedList = new List<Institution>{
                new Institution
                {
                    Id = 1,
                    MenuId = 1,
                    Name = "Гараж",
                    Address = "Мандарин",
                    CreationDate = new DateTime(2015, 5, 12),
                    City = "Гомель",
                    ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
                },
                new Institution
                {
                    Id = 2,
                    MenuId = 2,
                    Name = "Бургер кинг",
                    Address = "Вокзал",
                    CreationDate = new DateTime(1999, 11, 12),
                    City = "Гомель",
                    ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
                },
                new Institution
                {
                    Id = 3,
                    MenuId = 3,
                    Name = "МакДак",
                    Address = "Хатаевича",
                    CreationDate = new DateTime(1997, 9, 6),
                    City = "Гомель",
                    ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
                },
            };

            // Act
            var result = _repository.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedList);
        }

        [Test]
        [Order(2)]
        public async Task GetByIdAsync_WhenDataIncorrect_ShouldReturnNull()
        {
            // Arrange

            // Act
            var entity = await _repository.GetByIdAsync(-9);

            // Assert
            Assert.That(entity, Is.Null);
        }

        [Test]
        [Order(3)]
        public async Task GetByIdAsync_WhenDataCorrect_ShouldReturnEntity()
        {
            // Arrange
            var expectedEntity = new Institution
            {
                Id = 1,
                MenuId = 1,
                Name = "Гараж",
                Address = "Мандарин",
                CreationDate = new DateTime(2015, 5, 12),
                City = "Гомель",
                ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
            };

            // Act
            var entity = await _repository.GetByIdAsync(1);

            // Assert
            entity.Should().BeEquivalentTo(expectedEntity);
        }

        [Test]
        [Order(4)]
        public async Task Update_WhenDataCorrect_ShouldUpdateEntity()
        {
            // Arrange
            var entity = new Institution
            {
                Id = 1,
                MenuId = 1,
                Name = "Гараж",
                Address = "Тест апдейт",
                CreationDate = new DateTime(2015, 5, 12),
                City = "Гомель",
                ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
            };

            // Act
            await _repository.UpdateAsync(entity);
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(entity));
        }

        [Test]
        [Order(5)]
        public async Task Delete_WhenDataCorrect_ShouldDeleteEntity()
        {
            // Arrange
            // Act
            await _repository.DeleteAsync(1);
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Create_WhenDataCorrect_ShouldCreateEntity()
        {
            // Arrange
            var entity = new Institution
            {
                Name = "Тест",
                Address = "Тест",
                CreationDate = DateTime.Now,
                City = "Тест",
                ExpectedDeliveryTime = new TimeSpan(1, 0, 0),
            };

            // Act
            var result = await _repository.CreateAsync(entity);
            entity.Id = result.Id;

            // Assert
            Assert.That(result, Is.EqualTo(entity));
        }
    }
}
