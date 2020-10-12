using Eventi.Common;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class PerformerVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public PerformerCategory PerformerCategory { get; set; }
    }
}
