using Eventi.Common;

namespace Eventi.Domain
{
    public class PurchaseType
    {
        public int Id { get; set; }
        public int PurchaseID { get; set; }
        public Purchase Purchase { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public int NumberOfTickets { get; set; }  // odredjuje korisnik
        public float Price { get; set; } // BrojKarta*ProdajaTip.CijenaTip
        ////ProdajaTip ?  // probati
        public int? SaleTypeID { get; set; }
        public SaleType SaleType { get; set; }

    }
}
