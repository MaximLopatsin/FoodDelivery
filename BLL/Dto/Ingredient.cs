using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
