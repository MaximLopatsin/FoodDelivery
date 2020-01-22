using System;

namespace DAL.Domain
{
    public class Order : BaseDomain
    {
        public int InstitutionId { get; set; }

        public DateTime CreationTime { get; set; }

        public string ClientId { get; set; }

        public string DispatcherId { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public decimal Cost { get; set; }

        public decimal DeliveryCost { get; set; }
    }
}
