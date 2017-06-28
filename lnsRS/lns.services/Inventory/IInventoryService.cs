using System.Collections.Generic;
using System.Threading.Tasks;

namespace lns.services.Inventory
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetInventoryAsync();
        Task<Inventory> GetInventoryItemAsync(int id);
        Task<Inventory> CreateInventoryItemAsync(Inventory item);
        Task<Inventory> DeleteInventoryItemAsync(int id);
    }
}
