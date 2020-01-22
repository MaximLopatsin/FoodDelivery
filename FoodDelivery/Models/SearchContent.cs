namespace FoodDelivery.Models
{
    public enum SearchType
    {
        Menu,
        Institution,
    }

    public class SearchContent
    {
        public SearchType SearchType { get; set; }

        public string Content { get; set; }
    }
}