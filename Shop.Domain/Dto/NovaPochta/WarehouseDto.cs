using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.NovaPochta
{
    public class WarehouseDto
    {
        public string Description { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PlaceMaxWeightAllowed { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
    }
}
