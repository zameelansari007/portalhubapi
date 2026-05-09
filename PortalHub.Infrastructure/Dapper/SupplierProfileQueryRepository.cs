using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;

namespace PortalHub.Infrastructure.Dapper
{
    public class SupplierProfileQueryRepository
        : ISupplierProfileQueryRepository
    {
        private readonly string _connectionString;

        public SupplierProfileQueryRepository(
            IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string missing");
        }

        public async Task<IEnumerable<SupplierProfileResponseDto>> GetAllAsync()
        {
            var sql = @"
SELECT
    sp.SupplierId,
    sp.GSTNumber,
    sp.BusinessName,
    sp.OfficeAddressLine1,
    sp.OfficeAddressLine2,

    sp.CountryId,
    c.CountryName,

    sp.StateId,
    s.StateName,

    sp.CityId,
    ct.CityName,

    sp.Pincode,
    sp.IsGSTVerified,
    sp.CreatedAt

FROM Portal.SupplierProfiles sp

INNER JOIN Master.Countries c
    ON sp.CountryId = c.CountryId

INNER JOIN Master.States s
    ON sp.StateId = s.StateId

INNER JOIN Master.Cities ct
    ON sp.CityId = ct.CityId

ORDER BY sp.SupplierId DESC";

            using var conn = new SqlConnection(_connectionString);

            return await conn.QueryAsync<SupplierProfileResponseDto>(sql);
        }

        public async Task<SupplierProfileResponseDto?> GetByIdAsync(
            long supplierId)
        {
            var sql = @"
SELECT
    sp.SupplierId,
    sp.GSTNumber,
    sp.BusinessName,
    sp.OfficeAddressLine1,
    sp.OfficeAddressLine2,

    sp.CountryId,
    c.CountryName,

    sp.StateId,
    s.StateName,

    sp.CityId,
    ct.CityName,

    sp.Pincode,
    sp.IsGSTVerified,
    sp.CreatedAt

FROM Portal.SupplierProfiles sp

INNER JOIN Master.Countries c
    ON sp.CountryId = c.CountryId

INNER JOIN Master.States s
    ON sp.StateId = s.StateId

INNER JOIN Master.Cities ct
    ON sp.CityId = ct.CityId

WHERE sp.SupplierId = @SupplierId";

            using var conn = new SqlConnection(_connectionString);

            return await conn.QueryFirstOrDefaultAsync
                <SupplierProfileResponseDto>(
                    sql,
                    new { SupplierId = supplierId });
        }
    }
}