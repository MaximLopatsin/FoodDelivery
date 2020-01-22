using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IRepository<DAL.Domain.Order> _orderRepository;
        private readonly IRepository<DAL.Domain.Institution> _institutionRepository;
        private readonly IRepository<DAL.Domain.OrderMeal> _orderMealRepository;
        private readonly IRepository<DAL.Domain.Meal> _mealRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<DAL.Domain.Order> orderRepository, IRepository<DAL.Domain.Institution> institutionRepository, IRepository<DAL.Domain.OrderMeal> orderMealRepository, IRepository<DAL.Domain.Meal> mealRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _institutionRepository = institutionRepository;
            _orderMealRepository = orderMealRepository;
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public List<Order> GetOrders()
        {
            var orders = _mapper.Map<List<Order>>(_orderRepository.GetAll().ToList());

            foreach (var item in orders)
            {
                var inst = Task.Run(() => _institutionRepository.GetByIdAsync(item.InstitutionId)).Result;
                item.InstitutionName = inst.Name;
            }

            return _mapper.Map<List<Order>>(orders);
        }

        public async Task CreateOrderAsync(Order order, List<OrderMeal> orderMeals)
        {
            var domain = _mapper.Map<DAL.Domain.Order>(order);
            var model = await _orderRepository.CreateAsync(domain);
            var orderMealsDomain = _mapper.Map<List<DAL.Domain.OrderMeal>>(orderMeals);
            foreach (var item in orderMealsDomain)
            {
                item.OrderId = model.Id;
                await _orderMealRepository.CreateAsync(item);
            }
        }

        public async Task UpdateOrderAsync(Order order, List<OrderMeal> orderMeals)
        {
            var domain = _mapper.Map<DAL.Domain.Order>(order);
            await _orderRepository.UpdateAsync(domain);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            var domain = _mapper.Map<DAL.Domain.Order>(order);
            await _orderRepository.DeleteAsync(domain.Id);
        }

        public List<Meal> GetMealsByOrderId(int id)
        {
            var orderMeals = _orderMealRepository.GetAll().Where(a => a.OrderId == id).ToList();
            var meals = new List<Meal>();

            foreach (var item in orderMeals)
            {
                var meal = Task.Run(() => _mealRepository.GetByIdAsync(item.MealId)).Result;
                meals.Add(new Meal
                {
                    Id = meal.Id,
                    Name = meal.Name,
                });
            }

            return meals;
        }

        public async Task UpdateJustOrderAsync(Order order)
        {
            var domain = _mapper.Map<DAL.Domain.Order>(order);
            await _orderRepository.UpdateAsync(domain);
        }
    }
}
