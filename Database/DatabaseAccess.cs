using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
namespace Iskolaryo.Database
{
    public class DatabaseAccess
    {
        public async Task<IEnumerable<T>> LoadSingleJointData<T, U>(string sql, Func<T, U, T> map, string connectionString, string splitOnString)
        {
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                var dataList = await connection.QueryAsync(sql, map, splitOn: splitOnString).ConfigureAwait(false);
                

                foreach (var item in dataList)
                {
                    Console.WriteLine(item);
                }

                return dataList.ToList();
            }
        }


        public Task ExecuteSQL<T>(string sql, T parameters, string connectionString)
        {
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                return connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
