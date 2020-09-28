using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class Order
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }

        [Display(Name = "Заведение")]
        public string InstitutionName { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationTime { get; set; }

        public string ClientId { get; set; }

        public string DispatcherId { get; set; }

        [Display(Name = "Статус заказа")]
        public DeliveryType DeliveryType { get; set; }

        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        [Display(Name = "Стоимость доставки")]
        public decimal DeliveryCost { get; set; }
    }
}
