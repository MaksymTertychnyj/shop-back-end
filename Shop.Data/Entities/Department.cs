using Shop.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public virtual int TargetType { get; set; } = (int)Enums.TargetType.Department;
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
