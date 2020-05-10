using FoodLavkaAdmin.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodLavkaAdmin.ViewModels
{
    public class OrderViewModel : Order
    {
        [Display(Name = "Клиент")]
        public Client Client { get; set; }

        [Display(Name = "Дата создания заказа")]
        public string DateString { get; set; }
    }
}
