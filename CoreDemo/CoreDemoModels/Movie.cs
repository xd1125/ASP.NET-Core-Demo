using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDemoModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CinemaId { get; set; }
        public string Starring { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
