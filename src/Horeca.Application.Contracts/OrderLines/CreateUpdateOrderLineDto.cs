﻿using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.OrderLines
{
    public class CreateUpdateOrderLineDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
    }
}