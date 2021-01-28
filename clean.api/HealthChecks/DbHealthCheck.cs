using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace clean.api.HealthChecks
{
    public class DbHealthCheck:IHealthCheck
    {
        public DbHealthCheck(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private string ConnectionString { get; }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var dataSource = Regex.Match(ConnectionString, @"Data Source=([A-Za-z0-9_.])", RegexOptions.IgnoreCase).Value;
            using var connection = new SqlConnection(ConnectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);
                var command = connection.CreateCommand();
                command.CommandText = "select 1";
                await command.ExecuteNonQueryAsync(cancellationToken);
                return HealthCheckResult.Healthy(dataSource);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(dataSource, ex);
            }
        }
    }
}
