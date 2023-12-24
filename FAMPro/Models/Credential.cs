using System.ComponentModel.DataAnnotations;

namespace FAMPro.Models
{
    public class Credential
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
