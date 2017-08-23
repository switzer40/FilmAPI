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
        public FilmViewModel(string title, short year, short length = 10)
        {
            Title = title;
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
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [Range(1850, 2100)]
        public short Year { get; set; }
        public short Length { get; set; }
        public override string SurrogateKey { get; set; }
    }
}
