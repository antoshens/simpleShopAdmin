using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLavkaAdmin.Models
{
    public class Client
    {
        public Client()
        {
            Id = -1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя клиента")]
        [Display(Name="Имя")]
        public string Name { get; set; }
        [Phone]
        [Display(Name="Телефон")]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Client))
                return false;

            return (obj as Client).Id == Id;
        }
    }
}
