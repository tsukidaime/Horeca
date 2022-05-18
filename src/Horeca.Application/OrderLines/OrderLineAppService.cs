using Horeca.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Horeca.OrderLines
{
    public class OrderLineAppService : CrudAppService<
        OrderLine,
        OrderLineDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateOrderLineDto>,
        IOrderLineAppService
    {
        public OrderLineAppService(IRepository<OrderLine, Guid> repository) : base(repository)
        {
        }
    }
}
