using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Group21ProjectMVC.Models;
using System.Data;

namespace Group21ProjectMVC.Data
{
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly string _connectionString;

        public RoleStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.OpenAsync(cancellationToken);
                using SqlCommand cmd = new("CreateOrUpdateUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", role.Id);
                cmd.Parameters.AddWithValue("UserName", role.Name);
                cmd.Parameters.AddWithValue("NormalizedUserName", role.NormalizedName);
                await connection.OpenAsync(cancellationToken);
                role.Id = (int)await cmd.ExecuteScalarAsync(cancellationToken);
                /*role.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [ApplicationRole] ([Name], [NormalizedName])
                    VALUES (@{nameof(ApplicationRole.Name)}, @{nameof(ApplicationRole.NormalizedName)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", role);*/
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                using SqlCommand cmd = new("CreateOrUpdateUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", role.Id);
                cmd.Parameters.AddWithValue("UserName", role.Name);
                cmd.Parameters.AddWithValue("NormalizedUserName", role.NormalizedName);
                await connection.OpenAsync(cancellationToken);
                await cmd.ExecuteNonQueryAsync(cancellationToken);
                /*await connection.ExecuteAsync($@"UPDATE [ApplicationRole] SET
                    [Name] = @{nameof(ApplicationRole.Name)},
                    [NormalizedName] = @{nameof(ApplicationRole.NormalizedName)}
                    WHERE [Id] = @{nameof(ApplicationRole.Id)}", role);*/
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                using SqlCommand cmd = new("DeleteRole", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", role.Id);
                await connection.OpenAsync(cancellationToken);
                await cmd.ExecuteNonQueryAsync(cancellationToken);
                /*await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [ApplicationRole] WHERE [Id] = @{nameof(ApplicationRole.Id)}", role);*/
            }

            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("FindRoleById", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", roleId);
            ApplicationRole? response = null;
            await connection.OpenAsync(cancellationToken);
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    response = MapToValue(reader);
                }
            }

            return response;
            /*await connection.OpenAsync(cancellationToken);
            return await connection.QuerySingleOrDefaultAsync<ApplicationRole>($@"SELECT * FROM [ApplicationRole]
                    WHERE [Id] = @{nameof(roleId)}", new { roleId });*/
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("FindRoleByName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("NormalizedName", normalizedRoleName);
            ApplicationRole? response = null;
            await connection.OpenAsync(cancellationToken);
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    response = MapToValue(reader);
                }
            }

            return response;
            /*await connection.OpenAsync(cancellationToken);
            return await connection.QuerySingleOrDefaultAsync<ApplicationRole>($@"SELECT * FROM [ApplicationRole]
                    WHERE [NormalizedName] = @{nameof(normalizedRoleName)}", new { normalizedRoleName });*/
        }
        private static ApplicationRole MapToValue(SqlDataReader reader)
        {
            return new ApplicationRole()
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                NormalizedName = reader["NormalizedName"].ToString()
            };
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
