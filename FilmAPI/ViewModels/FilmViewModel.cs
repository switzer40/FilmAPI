using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class FilmViewModel : BaseViewModel
    {
        public FilmViewModel()
        {
        }
        public FilmViewModel(string title, short year = 0, short length = 0)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        public FilmViewModel(short year, string title = null, short length = 0)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        public FilmViewModel(short year, short length = 0)
        {
            Title = null;
            Year = year;
            Length = length;
        }
        
        public FilmViewModel(Film f, string key)
        {
            Title = f.Title;
            Year = f.Year;
            Length = f.Length;
            SurrogateKey = key;
        }
        
        
        public string Title { get; set; }
       
        public short Year { get; set; }
        public short Length { get; set; }
        public override string SurrogateKey { get; set; }
    }
}
