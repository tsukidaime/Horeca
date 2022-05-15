using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Addresses
{
    public class AddressDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Block { get; set; }
        public string Region { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
    }
}
