using Dapper;
using lns.services.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace lns.services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository _repo;

        public InventoryService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Inventory>> GetInventoryAsync()
        {
            // execute the stored procedure called GetEmployees
            return await _repo.WithConnection(async c =>
            {
                // map the result from stored procedure to Employee data model
                var results = await c.QueryAsync<Inventory>("GetInventory", commandType: CommandType.StoredProcedure);
                return results;
            });
        }
        public async Task<Inventory> GetInventoryItemAsync(int id)
        {
            return await _repo.WithConnection(async c => {
                var p = new DynamicParameters();
                p.Add("Id", id, DbType.Int32);
                var item = await c.QueryAsync<Inventory>(
                    sql: "GetInventory",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return item.AsList()[0];
            });
        }

        public async Task<Inventory> CreateInventoryItemAsync(Inventory item)
        {
            return await _repo.WithConnection(async c => {
                var p = new DynamicParameters();
                p.Add("Id", item.Id, DbType.Int32);
                p.Add("Name_en", item.Name_en, DbType.String);
                p.Add("Description_en", item.Description_en, DbType.String);
                p.Add("Name_ru", item.Name_ru, DbType.String);
                p.Add("Description_ru", item.Description_ru, DbType.String);
                p.Add("ImageName", item.ImageName, DbType.String);
                p.Add("IsActive", item.IsActive, DbType.Boolean);
                p.Add("Price", item.Price, DbType.Decimal);
                await c.QueryAsync<Inventory>(
                    sql: "CreateInventoryItem",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return item;
            });
        }

        public async Task<Inventory> DeleteInventoryItemAsync(int id)
        {
            return await _repo.WithConnection(async c => {
                var p = new DynamicParameters();
                p.Add("Id", id, DbType.Int32);
                var item = await c.QueryAsync<Inventory>(
                    sql: "DeleteInventoryItem",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return item.AsList()[0];
            });

            /*
                        string sql = "Delete from Inventories where Id = @Id";

                        return await _repo.WithConnection(async c => {
                            var p = new DynamicParameters();
                            p.Add("Id", id, DbType.Int32);
                            await c.QueryAsync<Inventory>(
                                sql: sql,
                                param: p,
                                commandType: CommandType.Text);
                            return 1;
                        });
            */
        }

    }
}