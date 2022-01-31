using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Horeca.Data;

/* This is used if database provider does't define
 * IHorecaDbSchemaMigrator implementation.
 */
public class NullHorecaDbSchemaMigrator : IHorecaDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
