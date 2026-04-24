using SmartInventory.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string Contact { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
