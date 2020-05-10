using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodLavkaAdmin.Models.ModelStorage
{
    public class SampleData
    {
        public static void Initialize(ModelContext context)
        {
            context.SaveChanges();
        }
    }
}
