using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Horeca.Blob
{
    public interface IFileAppService : IApplicationService
    {
        Task SaveBlobAsync(SaveBlobInputDto input);

        Task<BlobDto> GetBlobAsync(GetBlobRequestDto input);
    }
}
