using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.GenderDTOs
{
    public class GenderCreateDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
