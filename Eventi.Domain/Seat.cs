namespace Eventi.Domain
{
    public class Seat
    {
        public int ID { get; set; }
        public int SectionID { get; set; }
        public Section Section { get; set; }
    }
}
