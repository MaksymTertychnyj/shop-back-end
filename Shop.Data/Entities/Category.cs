using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shop.Data.Enums;

namespace Shop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public virtual int TargetType { get; set; } = (int)Enums.TargetType.Category;
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public JsonModel? JsonModel { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
