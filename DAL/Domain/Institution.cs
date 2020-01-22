using System;

namespace DAL.Domain
{
    public class Institution : BaseDomain
    {
        public int MenuId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public TimeSpan ExpectedDeliveryTime { get; set; }
    }
}
