using Horeca.Models;
using Horeca.Permissions;
using Microsoft.EntityFrameworkCore;
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
            GetAddressListDto,
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

        public override async Task<PagedResultDto<AddressDto>> GetListAsync(GetAddressListDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(input.UserId != null, x => x.UserId == (Guid)input.UserId);

            var totalCount = await query.CountAsync();
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var addressList = await query.ToListAsync();

            return new PagedResultDto<AddressDto>(
                totalCount,
                ObjectMapper.Map<List<Address>, List<AddressDto>>(addressList)
            );
        }
    }
}
