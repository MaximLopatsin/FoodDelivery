using System.ComponentModel.DataAnnotations;

namespace BLL.Dto
{
    public enum DeliveryType
    {
        [Display(Name = "Новый")]
        New,

        [Display(Name = "В процессе")]
        Processing,

        [Display(Name = "Доставлен")]
        Success,

        [Display(Name = "Отменен")]
        Aborted,
    }
}
