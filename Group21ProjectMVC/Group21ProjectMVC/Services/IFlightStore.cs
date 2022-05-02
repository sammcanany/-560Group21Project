namespace Group21ProjectMVC.Services
{
    public interface IFlightStore<TFlight> : IDisposable where TFlight : class
    {
        Task<bool> AddFlightsAsync(IEnumerable<TFlight> flights, CancellationToken cancellationToken);

        Task<IEnumerable<TFlight>> GetFlightsAsync(string FromLocation, string ToLocation, int SeatsRequired, DateTime? DepartureDate, CancellationToken cancellationToken);

        Task<TFlight> GetFlightByIdAsync(int ID, CancellationToken cancellationToken);

        Task<IEnumerable<int>> GetSeatsAvailableByFlightIdAsync(int ID, CancellationToken cancellationToken);

        Task<List<string>> GetAirlinesAsync(CancellationToken cancellationToken);

        Task<string> SetSeatsTakenByFlightIdAsync(int ID, CancellationToken cancellationToken);
    }
}
