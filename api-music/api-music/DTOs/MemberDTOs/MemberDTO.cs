using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.MemberDTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Avatar { get; set; }
    }
}
