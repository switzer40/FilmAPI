using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Exceptions
{
    public class UnknownFilmException : Exception
    {
        public UnknownFilmException(string title, short year) : base($"Unknown film Title: {title} Year: {year}")
        {
            Title = title;
            Year = year;
        }
        public string Title { get; set; }
        public short Year { get; set; }        
    }
}
