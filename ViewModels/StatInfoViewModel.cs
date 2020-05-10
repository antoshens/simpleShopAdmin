using FoodLavkaAdmin.Models;

namespace FoodLavkaAdmin.ViewModels
{
    public class StatInfoViewModel
    {
        public string OrdrDay { get; set; }
        public int TimePrice { get; set; }
        public Client Client { get; set; }
        public int ClientSumPrice { get; set; }
    }
}
