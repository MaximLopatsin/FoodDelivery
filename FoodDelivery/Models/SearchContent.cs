using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public enum SearchType
    {
        [Display(Name = "Меню")]
        Menu,
        [Display(Name = "Заведения")]
        Institution,
    }

    public class SearchContent
    {
        [Display(Name = "Тип поиска")]
        public SearchType SearchType { get; set; }

        public string Content { get; set; }
    }
}