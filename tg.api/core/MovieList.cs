using System;
using System.Collections.Generic;

namespace core
{
    public class MovieList
    {
        public List<Movie> Movies { get; set; }
    }

    public class Movie
    {
        public string title { get; set; }
        public int year { get; set; }
        public string[] cast { get; set; }
        public string[] genres { get; set; }

    }
}