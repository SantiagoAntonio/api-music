﻿using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.CDDTOS
{
    public class CDDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public DateTime Launch { get; set; }
        public string Poster { get; set; }
    }
}
