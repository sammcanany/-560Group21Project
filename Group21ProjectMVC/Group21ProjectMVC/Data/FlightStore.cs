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

        public async Task<bool> AddOrEditFlightsAsync(IEnumerable<ApplicationFlight> flights, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new("Flights.CreateOrUpdateFlights", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@ImportTable", ConvertData(flights.ToList()));
            sqlParameter.SqlDbType = SqlDbType.Structured;
            await connection.OpenAsync(cancellationToken);
            await cmd.ExecuteScalarAsync(cancellationToken);
            return true;
        }

        public static DataTable ConvertData(List<ApplicationFlight> ValuesList)
        {
            DataTable dtData = new DataTable();
            var objectReference = ValuesList.GetType().GetGenericArguments()[0];
            var properties = objectReference.GetProperties();
            foreach (var prop in properties)
            {
                dtData.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in ValuesList)
            {
                var dataArray = new List<object>();
                foreach (var prop in properties)
                {
                    dataArray.Add(prop.GetValue(item));
                }

                dtData.Rows.Add(dataArray.ToArray());
            }

            return dtData;
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
