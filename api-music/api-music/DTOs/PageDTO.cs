namespace api_music.DTOs
{
    public class PageDTO
    {
        public int Page { get; set; } = 1;
        private int cantRegPerPage = 10;
        private readonly int cantMaxRegPerPage = 50;

        public int CantRegPerPage
        {
            get => cantRegPerPage;
            set
            {
                cantRegPerPage = (value > cantMaxRegPerPage)? cantMaxRegPerPage : value;
            }
        }
    }
}
