using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class Institution
    {
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }

        public int MenuId { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Ожидаемое время доставки")]
        public TimeSpan ExpectedDeliveryTime { get; set; }
    }
}
