namespace Ticketing.Web.Models
{
    public class TicketModel
    {
        public string Id { get; set; }
        public string TicketURL { get; set; }
        public bool Queued { get; set; }
    }
}