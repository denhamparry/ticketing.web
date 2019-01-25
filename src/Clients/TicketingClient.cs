using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Ticketing.Web.Models;

namespace Ticketing.Web.Clients
{
    public class TicketingClient
    {
        private IOptionsSnapshot<AppConfiguration> _appSettings;
        public TicketingClient(IOptionsSnapshot<AppConfiguration> appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task<TicketModel> GetTicket()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri($"{_appSettings.Value.TicketingAPI}/tickets");

                var ticket = new TicketModel();
                var response = await client.PostAsJsonAsync(url,ticket);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                return JsonConvert.DeserializeObject<TicketModel>(json);
            }
        }
    }
}