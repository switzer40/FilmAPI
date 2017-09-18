using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class FilmViewModel : BaseViewModel
    {
        public FilmViewModel() : base()
        {
        }
        public FilmViewModel(string title, short year = 0, short length = 0) : base()
        {
            Title = title;
            Year = year;
            Length = length;
            _key = _keyService.ConstructFilmSurrogateKey(Title, Year);
        }
        public FilmViewModel(Film f) : base()
        {
            Title = f.Title;
            Year = f.Year;
            _key = _keyService.ConstructFilmSurrogateKey(Title, Year);
        }
        public FilmViewModel(short year, short length = 0) : base()
        {
            Title = null;
            Year = year;
            Length = length;
            _key = _keyService.ConstructFilmSurrogateKey(Title, Year);
        }

        private string _key;        
        public string Title { get; set; }       
        public short Year { get; set; }
        public short Length { get; set; }
        public override string SurrogateKey { get { return _key; } }
    }
}
