using FilmAPI.Core.SharedKernel;
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
        private readonly IKeyService _keyService;
        public FilmViewModel(IKeyService keyService, string title, short year, short length = 0)
        {
            _keyService = keyService;
            Title = title;
            Year = year;
            Length = length;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public short Year { get; set; }
        public short Length { get; set; }
        public override string SurrogateKey => _keyService.ConstructFilmSurrogateKey(this);
    };
}

