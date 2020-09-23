using Eventi.Common;

namespace Eventi.Contracts.V1.Responses
{
    public class PerformerResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PerformerCategory PerformerCategory { get; set; }
    }
}
