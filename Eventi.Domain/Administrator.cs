namespace Eventi.Domain
{
    public class Administrator
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }

    }
}
