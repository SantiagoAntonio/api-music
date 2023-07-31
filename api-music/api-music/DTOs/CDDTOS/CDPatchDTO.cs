using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.CDDTOS
{
    public class CDPatchDTO
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool Status { get; set; }
        public DateTime Launch { get; set; }
    }
}
