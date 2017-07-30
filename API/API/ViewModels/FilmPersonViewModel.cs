using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Core.Interfaces;

namespace API.ViewModels
{
    public class FilmPersonViewModel : BaseViewModel
    {
        public FilmPersonViewModel(IKeyService service,
                                   int filmId,
                                   int personId,
                                   string role) : base(service)
        {
            FilmId = filmId;
            PersonId = personId;
            Role = role;
        }

        public override string SurrogateKey()
        {
            return _keyService.ConstructFilmPersonSurrogateKey(FilmId, PersonId, Role);
        }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
