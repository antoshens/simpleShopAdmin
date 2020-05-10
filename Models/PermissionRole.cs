using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodLavkaAdmin.Models
{
    public enum PermissionRoleEnum
    {
        Administrator = 0,
        Sales
    }

    public class PermissionRole
    {
        public int Id { get; set; }
        public PermissionRoleEnum Role { get; set; }
    }
}
