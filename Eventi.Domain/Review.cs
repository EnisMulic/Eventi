namespace Eventi.Domain
{
    public class Review
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public int PurchaseID { get; set; }
        public Purchase Purchase { get; set; }
    }
}
