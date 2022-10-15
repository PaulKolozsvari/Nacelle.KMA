using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Managers
{
    public interface IDataMigrationManager
    {
        Task MigrateDataAsync();
    }
}