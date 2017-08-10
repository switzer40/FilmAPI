using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class FilmViewModel : BaseViewModel
    {
        public FilmViewModel(string title, short year, short length = 0)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public short Year { get; set; }
        public short Length { get; set; }
    }
}
