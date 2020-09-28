using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class Menu
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }

        [Display(Name = "Дата создания меню")]
        public DateTime CreationDate { get; set; }

        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
