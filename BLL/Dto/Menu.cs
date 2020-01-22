using System;
using System.Collections.Generic;

namespace BLL.Dto
{
    public class Menu
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
