using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAMPro.Models
{
    public class FixedAssetsMaster
    {
        [Key]
        public int FixedAssetId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string FixedAssetName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = "Date Acquired")]
        [Required]
        public DateTime AcquiredDate { get; set; }
        [Display(Name = "Description")]
        public string Narration { get; set; } = string.Empty;
        [Display(Name = "Lifetime (Years)")]
        public decimal Life { get; set; }
        [Display(Name = "Location ID")]
        [Required]
        public string LocationId { get; set; } = string.Empty;
        [Display(Name = "Assigned To")]
        [Required]
        public string AssignedTo { get; set; } = string.Empty;
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Quantity")]
        [Required]
        public int Qty { get; set; }
        [Display(Name = "Disposed Quantity")]
        [Required]
        public int DisposedQty { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created On")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedUser { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = "Last Edited On")]
        public DateTime LastEditedDate { get; set; }
        [Display(Name = "Last Edited By")]
        public string LastEditedUser { get; set; } = string.Empty;
        [ForeignKey("LocationId")]
        public virtual LocationMaster? locationMaster { get; set; }
    }
}