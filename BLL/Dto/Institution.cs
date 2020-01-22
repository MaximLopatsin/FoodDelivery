using System;

namespace BLL.Dto
{
    public class Institution
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MenuId { get; set; }

        public DateTime CreationDate { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public TimeSpan ExpectedDeliveryTime { get; set; }
    }
}
