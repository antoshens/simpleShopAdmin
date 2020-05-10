using FoodLavkaAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodLavkaAdmin.ViewModels
{
    public class UserViewModel : User
    {
        public PermissionRole PermRole { get; set; }
    }
}
