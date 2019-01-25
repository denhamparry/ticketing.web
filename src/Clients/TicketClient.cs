using System.Threading.Tasks;
using Ticketing.Web.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;

namespace Ticketing.Web.Clients
{
    public class TicketClient
    {
        private IOptionsSnapshot<AppConfiguration> _appSettings;
        public TicketClient(IOptionsSnapshot<AppConfiguration> appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task<TicketModel> GetTickets()
        {
            var url = _appSettings.Value.TicketingAPI;
            var result = await $"{_appSettings.Value.TicketingAPI}/tickets".PostJsonAsync(new { ticketUrl = "https://www.youtube.com" }).ReceiveJson<TicketModel>();
            return result;
        }
    }
}