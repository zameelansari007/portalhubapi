using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace PortalHub.Infrastructure.Dapper.Repositories
{
    public class DapperQueryRepository<T> : IQueryRepository<T>
        where T : class
    {
        private readonly string _connectionString;

        public DapperQueryRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DB connection missing");
        }

        private SqlConnection CreateConnection()
            => new SqlConnection(_connectionString);

        // ✅ YOUR METHOD — CORRECT PLACE
        //private static (string schema, string table) ResolveTable()
        //{
        //    var attr = typeof(T)
        //        .GetCustomAttribute<TableMapAttribute>();

        //    if (attr == null)
        //        throw new InvalidOperationException(
        //            $"TableMapAttribute missing on {typeof(T).Name}");

        //    return (attr.Schema, attr.TableName);
        //}

        //private static string ResolveKeyColumn()
        //{
        //    var attr = typeof(T).GetCustomAttribute<KeyColumnAttribute>();

        //    if (attr == null)
        //        throw new InvalidOperationException(
        //            $"KeyColumnAttribute missing on {typeof(T).Name}");

        //    return attr.ColumnName;
        //}

        private static (string schema, string table) ResolveTable()
        {
            var attr = typeof(T).GetCustomAttribute<TableAttribute>();

            if (attr == null)
                throw new InvalidOperationException(
                    $"Table attribute missing on {typeof(T).Name}");

            var schema = string.IsNullOrWhiteSpace(attr.Schema)
                ? "dbo"
                : attr.Schema;

            return (schema, attr.Name);
        }

        private static string ResolveKeyColumn()
        {
            var keyProperty = typeof(T)
                .GetProperties()
                .FirstOrDefault(p =>
                    p.GetCustomAttribute<KeyAttribute>() != null);

            if (keyProperty == null)
                throw new InvalidOperationException(
                    $"Key attribute missing on {typeof(T).Name}");

            return keyProperty.Name;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var (schema, table) = ResolveTable();
            var sql = $"SELECT * FROM [{schema}].[{table}]";

            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<T>(sql);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            var (schema, table) = ResolveTable();
            var keyColumn = ResolveKeyColumn();

            var sql = $@"
            SELECT * 
            FROM [{schema}].[{table}]
            WHERE [{keyColumn}] = @Id";

            using var conn = new SqlConnection(_connectionString);
            return await conn.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
        }
    }
}
