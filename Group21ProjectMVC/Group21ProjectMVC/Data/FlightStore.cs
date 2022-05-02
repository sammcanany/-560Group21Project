using Group21ProjectMVC.Models;
using Group21ProjectMVC.Services;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Group21ProjectMVC.Data
{
    public class FlightStore : IFlightStore<ApplicationFlight>
    {
        private readonly string _connectionString;

        public FlightStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> AddFlightsAsync(IEnumerable<ApplicationFlight> flights, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.AddFlights", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@ImportTable", ConvertData(flights.ToList()));
            sqlParameter.SqlDbType = SqlDbType.Structured;
            await connection.OpenAsync(cancellationToken);
            await cmd.ExecuteScalarAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<ApplicationFlight>> GetFlightsAsync(string FromLocation, string ToLocation, int SeatsRequired, DateTime? DepartureDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.FlightSearch", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FromLocation", FromLocation);
            cmd.Parameters.AddWithValue("ToLocation", ToLocation);
            cmd.Parameters.AddWithValue("SeatsRequired", SeatsRequired);
            cmd.Parameters.AddWithValue("DepartureDate", DepartureDate);
            IList<ApplicationFlight> response = new List<ApplicationFlight>();
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

        public async Task<ApplicationFlight> GetFlightByIdAsync(int ID, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.GetFlightByID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FlightID", ID);
            ApplicationFlight response = new();
            await connection.OpenAsync(cancellationToken);
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    response = MapToValue(reader);
                }
            }

            return response;
        }

        public async Task<IEnumerable<int>> GetSeatsAvailableByFlightIdAsync(int ID, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.GetSeatsAvailableByFlightId", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FlightID", ID);
            IList<int> response = new List<int>();
            await connection.OpenAsync(cancellationToken);
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    response.Add((int)reader["SeatNumber"]);
                }
            }
            return response;
        }

        public async Task<string> SetSeatsTakenByFlightIdAsync(int ID, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using SqlCommand cmd = new("Flights.SetSeatsTakenByFlightId", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("FlightID", ID);
                IList<int> response = new List<int>();
                await connection.OpenAsync(cancellationToken);
                await cmd.ExecuteScalarAsync(cancellationToken);
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            return "success";
        }

        public async Task<List<string>> GetAirlinesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.GetAirlines", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            List<string> response = new();
            await connection.OpenAsync(cancellationToken);
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    response.Add(reader["Name"].ToString());
                }
            }

            return response;
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
        #region Helpers
        private static ApplicationFlight MapToValue(SqlDataReader reader)
        {
            return new ApplicationFlight()
            {
                Id = (int)reader["Id"],
                FlightNumber = reader["FlightNumber"].ToString(),
                DepartingAirportCode = reader["DepartingAirportCode"].ToString(),
                DestinationAirportCode = reader["DestinationAirportCode"].ToString(),
                Airline = reader["Airline"].ToString(),
                DepartureDate = Convert.ToDateTime(reader["DepartureDate"].ToString()),
                DepartureTime = Convert.ToDateTime(reader["DepartureTime"].ToString()),
                ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"].ToString()),
                Capacity = (int)reader["Capacity"],
                SeatsTaken = (int)reader["SeatsTaken"],
                Price = (decimal)reader["Price"]
            };
        }

        public static DataTable ConvertData(List<ApplicationFlight> ValuesList)
        {
            DataTable dtData = new();
            var objectReference = ValuesList.GetType().GetGenericArguments()[0];
            var properties = objectReference.GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "SeatsNotAvailable")
                {
                    dtData.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            foreach (var item in ValuesList)
            {
                var dataArray = new List<object>();
                foreach (var prop in properties)
                {
                    if (prop.Name != "Id" && prop.Name != "SeatsNotAvailable")
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
