﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.NovaPochta
{
    public class RequestDto
    {
        public string modelName { get; set; } = string.Empty;
        public string calledMethod { get; set; } = string.Empty;
        public object? methodProperties { get; set; }
    }
}
