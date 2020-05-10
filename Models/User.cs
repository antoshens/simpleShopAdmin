using System;
using System.ComponentModel.DataAnnotations;

namespace FoodLavkaAdmin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int PermRoleId { get; set; }
        public DateTime LastActiveTime { get; set; }
    }
}
