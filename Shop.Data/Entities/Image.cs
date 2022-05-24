using Shop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public byte[]? ImageData { get; set; }
        public int TargetId { get; set; }
        public int TargetType { get; set; }
    }
}
