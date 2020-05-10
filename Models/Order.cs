using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLavkaAdmin.Models
{
    public class Order
    {
        public Order()
        {
            Id = -1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required (ErrorMessage = "Не указан адрес")]
        [Display(Name ="Адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Клиент не выбран")]
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }
        [Display(Name = "Описание заказа")]
        [Required(ErrorMessage = "Не указано описание заказа")]
        public string Description { get; set; }
        [Display(Name = "Сумма заказа")]
        [Range(1, 1000000, ErrorMessage = "Недопустимая сумма")]
        [Required(ErrorMessage = "Не указана сумма заказа")]
        public int Price { get; set; }
        [Display(Name = "Дата создания заказа")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Не указана дата получения заказа")]
        public DateTime Date { get; set; }
    }
}
