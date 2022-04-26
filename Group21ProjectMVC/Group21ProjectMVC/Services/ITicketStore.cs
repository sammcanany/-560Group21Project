namespace Group21ProjectMVC.Services
{
    public interface ITicketStore<TTicket> : IDisposable where TTicket : class
    {
        Task<string> AddTicketsAsync(IEnumerable<TTicket> Tickets, CancellationToken cancellationToken);
        Task<IEnumerable<TTicket>> GetTicketsByUserIdAsync(int UserId, CancellationToken cancellationToken);
    }
}
