using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Addresses
{
    public  class GetAddressListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
    }
}
