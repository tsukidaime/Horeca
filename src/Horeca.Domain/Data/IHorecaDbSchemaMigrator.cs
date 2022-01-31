using System.Threading.Tasks;

namespace Horeca.Data;

public interface IHorecaDbSchemaMigrator
{
    Task MigrateAsync();
}
