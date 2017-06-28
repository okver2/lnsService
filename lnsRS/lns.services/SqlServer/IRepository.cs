using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace lns.services.SqlServer
{
    public interface IRepository
    {
        Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData);
        SqlConnection GetConnection(bool multipleActiveResultSets = false);

    }
}