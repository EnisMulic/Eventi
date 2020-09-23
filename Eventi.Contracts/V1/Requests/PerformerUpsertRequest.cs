using Eventi.Common;

namespace Eventi.Contracts.V1.Requests
{
    public class PerformerUpsertRequest
    {
        public string Name { get; set; }
        public PerformerCategory PerformerCategory { get; set; }
    }
}
