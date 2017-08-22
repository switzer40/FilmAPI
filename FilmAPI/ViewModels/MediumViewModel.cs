using FilmAPI.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FilmAPI.ViewModels
{
    public class MediumViewModel : BaseViewModel
    {
        public MediumViewModel()
        {
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
