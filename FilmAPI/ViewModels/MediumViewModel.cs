using FilmAPI.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System;

namespace FilmAPI.ViewModels
{
    public class MediumViewModel : BaseViewModel
    {
        private string title;
        private short year;

        public MediumViewModel()
        {
        }

        public MediumViewModel(string title, short year, string mediumType)
        {
            FilmTitle = title;
            FilmYear = year;
            MediumType = mediumType;
        }
        public MediumViewModel(Film f, string mediumType, string key)
        {
            FilmTitle = f.Title;
            FilmYear = f.Year;
            MediumType = mediumType;
            SurrogateKey = key;
        }

        public MediumViewModel(string filmTitle, short filmYear, string mediumType, string location = "")
        {
            FilmTitle = filmTitle;
            FilmYear = filmYear;
            MediumType = mediumType;
            Location = location;
        }

        public MediumViewModel(string title, short year)
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
        public override string SurrogateKey { get; set; }
    }
}
