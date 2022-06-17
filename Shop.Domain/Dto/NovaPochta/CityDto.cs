using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.NovaPochta
{
    public class CityDto
    {
        public string Description { get; set; } = string.Empty;
        public string Ref { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string SettlementType { get; set; } = string.Empty;
        public string CityID { get; set; } = string.Empty;
        public string SettlementTypeDescription { get; set; } = string.Empty;
        public string AreaDescription { get; set; } = string.Empty;
    }
}
