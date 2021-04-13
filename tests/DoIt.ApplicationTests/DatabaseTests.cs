using System;
using System.Threading.Tasks;
using DoIt.ApplicationTests.Common;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Xunit;

namespace DoIt.ApplicationTests
{
    [Collection(nameof(SliceFixture))]
    public class DatabaseTests
    {
        private readonly SliceFixture _fixture;

        public DatabaseTests(SliceFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task CanConnectToLocalPostgres()
        {

            var configuration = _fixture.Scope.ServiceProvider.GetService<IConfiguration>()
                                ?? throw new NullReferenceException("Could not get DbConnectionFactory service");
            
            await using var conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("SELECT 1", conn);
            var result1 = (int?)await cmd.ExecuteScalarAsync();
            result1.Should().Be(1);

        }
    }
}