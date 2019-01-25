using System.Threading.Tasks;
using Ticketing.Web.Models;
using Flurl.Http;

namespace Ticketing.Web.Clients
{
    public class TicketClient
    {
        public async Task<TicketModel> GetTickets()
        {
            var result = await "http://localhost:5002/tickets".PostJsonAsync(new { ticketUrl = "https://www.youtube.com" }).ReceiveJson<TicketModel>();
            return result;
        }
    }
}