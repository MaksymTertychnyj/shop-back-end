using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
