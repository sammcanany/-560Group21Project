using Group21ProjectMVC.Models;
using Group21ProjectMVC.Services;
using System.Data;
using System.Data.SqlClient;

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
            DataTable dtData = new DataTable();
            var objectReference = ValuesList.GetType().GetGenericArguments()[0];
            var properties = objectReference.GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name != "Id")
                {
                    dtData.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            foreach (var item in ValuesList)
            {
                var dataArray = new List<object>();
                foreach (var prop in properties)
                {
                    if (prop.Name != "Id")
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
