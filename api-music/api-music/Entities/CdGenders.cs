namespace api_music.Entities
{
    public class CdGenders
    {
        public int GenderId { get; set; }
        public int CDId { get; set; }
        public Gender Gender { get; set; }
        public CD CD { get; set; }
    }
}
