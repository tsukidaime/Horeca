using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Horeca.Supplier
{
    public class SupplierDto : EntityDto<Guid>
    {
        public string CompanyName { get; set; }
        public string BIN { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
    }
}
