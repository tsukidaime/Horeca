using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Horeca.Addresses
{
    public class CreateUpdateAddressDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Building { get; set; }
        public string Block { get; set; }
        public string Region { get; set; }
        public string Comment { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
