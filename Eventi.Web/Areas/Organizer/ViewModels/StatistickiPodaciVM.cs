using System.Collections.Generic;

namespace Eventi.Web.Areas.Organizer.ViewModels
{
    public class StatistickiPodaciVM
    {

        public List<Rows> Data { get; set; } = new List<Rows>();

        public class Rows
        {
            public string EventName { get; set; }
            public int TicketsSold { get; set; }
            public float SumTotal { get; set; }
        }
    }
}
