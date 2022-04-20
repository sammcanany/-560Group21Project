namespace Group21ProjectMVC.Services
{
    public interface IFlightStore<TFlight> : IDisposable where TFlight : class
    {
        Task<bool> AddOrEditFlightsAsync(IEnumerable<TFlight> flights, CancellationToken cancellationToken);
    }
}
