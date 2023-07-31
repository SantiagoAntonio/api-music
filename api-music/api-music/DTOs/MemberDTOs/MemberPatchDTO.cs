using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.MemberDTOs
{
    public class MemberPatchDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
