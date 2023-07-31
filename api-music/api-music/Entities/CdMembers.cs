namespace api_music.Entities
{
    public class CdMembers
    {
        public int MemberId { get; set; }
        public int CDId { get; set; }

        public string Responsability { get; set; }
        public int Order { get; set;}

        public Member Member { get; set; }
        public CD CD { get; set; }
    }
}
