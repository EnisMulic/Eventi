namespace Eventi.Domain
{
    public class EventPerformer
    {
        public int PerformerID { get; set; }
        public Performer Performer { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
}
