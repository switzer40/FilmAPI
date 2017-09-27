using FilmAPI.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using FilmAPI.Interfaces;

namespace FilmAPI.ViewModels
{
    public class MediumViewModel : BaseViewModel
    {
        private string title;
        private short year;

        public MediumViewModel() : base()
        {
        }

        public MediumViewModel(string title, short year, string mediumType) : base()
        {
            FilmTitle = title;
            FilmYear = year;
            MediumType = mediumType;
            _key = _keyService.ConstructMediumSurrogateKey(FilmTitle, FilmYear, MediumType);
        }
        public MediumViewModel(Film f, string mediumType) : base()
        {
            FilmTitle = f.Title;
            FilmYear = f.Year;
            MediumType = mediumType;
            _key = _keyService.ConstructMediumSurrogateKey(FilmTitle, FilmYear, MediumType);
        }

        public MediumViewModel(string filmTitle,
                              short filmYear,
                              string mediumType,
                              string location = "") : base()
        {
            FilmTitle = filmTitle;
            FilmYear = filmYear;
            _key = _keyService.ConstructMediumSurrogateKey(FilmTitle, FilmYear, MediumType);
            MediumType = mediumType;
            Location = location;
        }

        public MediumViewModel(string title, short year) : base()
        {
            this.title = title;
            this.year = year;
        }

       

        [Required]
        public string FilmTitle { get; set; }
        [Required]
        public short FilmYear { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
        private string _key;
        public override string SurrogateKey { get { return _key; } }
    }
}
