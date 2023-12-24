using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace FAMPro.Models
{
    public class LocationMaster
    {
        [Key]
        [Display(Name = "Location ID")]
        public string LocationId { get; set; } = string.Empty;
        [Display(Name = "Location")]
        public string LocationName { get; set; } = string.Empty;
        public ICollection<FixedAssetsMaster>? FixedAssets { get; set; }
    }
}
