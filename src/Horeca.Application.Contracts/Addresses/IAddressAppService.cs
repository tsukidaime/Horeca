using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Horeca.Addresses
{
    public interface IAddressAppService
        : ICrudAppService< 
            AddressDto, 
            Guid, //Primary key of the address entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateAddressDto> //Used to create/update a address
    {
    }
}
