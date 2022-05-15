using Horeca.Models;
using Horeca.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Horeca.Addresses
{
    public class AddressAppService
        : CrudAppService<
            Address,
            AddressDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateAddressDto>,
        IAddressAppService
    {
        public AddressAppService(IRepository<Address, Guid> repository) : base(repository)
        {
            GetPolicyName = HorecaPermissions.AddressRead;
            CreatePolicyName = HorecaPermissions.AddressCreate;
            UpdatePolicyName = HorecaPermissions.AddressEdit;
            DeletePolicyName = HorecaPermissions.AddressDelete;
        }
    }
}
