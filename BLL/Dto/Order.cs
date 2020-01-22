using System;

namespace BLL.Dto
{
    public class Order
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }

        public string InstitutionName { get; set; }

        public DateTime CreationTime { get; set; }

        public string ClientId { get; set; }

        public string DispatcherId { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public decimal Cost { get; set; }

        public decimal DeliveryCost { get; set; }
    }
}
