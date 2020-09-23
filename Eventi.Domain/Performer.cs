using Eventi.Common;

namespace Eventi.Domain
{
    public class Performer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PerformerCategory PerformerCategory { get; set; }
    }
}
