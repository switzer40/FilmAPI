using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class FilmViewModel :BaseViewModel
    {
        public FilmViewModel(IKeyService service,  string title, short year, short length = 0) :base(service)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        public override string SurrogateKey()
        {
            return _keyService.ConstructFilmSurrogateKey(Title, Year);
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public short Year { get; set; }
        public short Length { get; set; }
    }
}
