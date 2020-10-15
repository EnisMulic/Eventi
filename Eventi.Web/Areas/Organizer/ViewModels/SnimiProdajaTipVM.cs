namespace Eventi.Web.Areas.Organizer.ViewModels
{
    public class SnimiProdajaTipVM
    {
        public string _tipKarteCombo { get; set; } 
        public int _ukupnoKarataTip { get; set; } 
        public int _brojProdatihKarataTip { get; set; } = 0;
        public float _cijenaTip { get; set; }
        public int _postojeSjedista { get; set; } 
        public int _eventID { get; set; }
    }
}
