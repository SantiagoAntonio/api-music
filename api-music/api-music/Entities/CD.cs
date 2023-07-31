using System.ComponentModel.DataAnnotations;

namespace api_music.Entities
{
    public class CD
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool Status { get; set; }
        public DateTime Launch { get; set; }
        public string Poster { get; set; }


    }
}
