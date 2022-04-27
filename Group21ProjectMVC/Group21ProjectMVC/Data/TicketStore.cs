using Group21ProjectMVC.Models;
using Group21ProjectMVC.Services;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Group21ProjectMVC.Data
{
    public class TicketStore : ITicketStore<ApplicationTicket>
    {
        private readonly string _connectionString;

        public TicketStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> AddTicketsAsync(IEnumerable<ApplicationTicket> Tickets, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using SqlCommand cmd = new("Flights.AddTickets", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@ImportTable", ConvertData(Tickets.ToList()));
                sqlParameter.SqlDbType = SqlDbType.Structured;
                await connection.OpenAsync(cancellationToken);
                var response = await cmd.ExecuteScalarAsync(cancellationToken);
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            return "success";
        }

        public async Task<IEnumerable<ApplicationTicket>> GetTicketsByUserIdAsync(int UserId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.GetTicketByUserId", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProfileID", UserId);
            IList<ApplicationTicket> response = new List<ApplicationTicket>();
            await connection.OpenAsync(cancellationToken);
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    response.Add(MapToValue(reader));
                }
            }

            return response;
        }

        public async Task<string> DeleteTicketAsync(ApplicationTicket ticket, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                using SqlCommand cmd = new("Flights.DeleteUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);
                await connection.OpenAsync(cancellationToken);
                await cmd.ExecuteNonQueryAsync(cancellationToken);
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
            return "success";
        }

        public async Task<string> DeleteAllUserTicketsAsync(int UserId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.DeleteAllUserTickets", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProfileId", UserId);
            await connection.OpenAsync(cancellationToken);
            await cmd.ExecuteNonQueryAsync(cancellationToken);
            return "success";
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
        #region Helpers
        private static ApplicationTicket MapToValue(SqlDataReader reader) => new()
        {
            Id = (int)reader["ProfileID"],
            ProfileID = (int)reader["ProfileID"],
            FlightID = (int)reader["FlightID"],
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            SeatNumber = (int)reader["SeatNumber"]
        };

        public static DataTable ConvertData(List<ApplicationTicket> ValuesList)
        {
            DataTable dtData = new();
            var objectReference = ValuesList.GetType().GetGenericArguments()[0];
            var properties = objectReference.GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name != "Profile" && prop.Name != "Flight" && prop.Name != "Id")
                {
                    dtData.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            foreach (var item in ValuesList)
            {
                var dataArray = new List<object>();
                foreach (var prop in properties)
                {
                    if (prop.Name != "Profile" && prop.Name != "Flight" && prop.Name != "Id")
                    {
                        dataArray.Add(prop.GetValue(item));
                    }
                }

                dtData.Rows.Add(dataArray.ToArray());
            }

            return dtData;
        }
        #endregion
    }
}
