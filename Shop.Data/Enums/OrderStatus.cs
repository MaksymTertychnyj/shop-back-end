using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Enums
{
    public enum OrderStatus
    {
        registered=1,
        negotiation=2,
        invoiced=3,
        paid=4,
        shipped=5,
        obtained=6
    }
}
