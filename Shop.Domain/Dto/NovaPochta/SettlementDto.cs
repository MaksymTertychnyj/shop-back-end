using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.NovaPochta
{
    public class SettlementDto
    {
        public string Ref { get; set; } = string.Empty;
        public string SettlementType { get; set; } = string.Empty;
        public string MyProperty { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SettlementTypeDescription { get; set; } = string.Empty;
        public string RegionsDescription { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Warehouse { get; set; } = string.Empty;
    }
}
