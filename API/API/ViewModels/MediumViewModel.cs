using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Core.Interfaces;

namespace API.ViewModels
{
    public class MediumViewModel : BaseViewModel
    {
        public MediumViewModel(IKeyService service, int filmId, string mediumType) : base(service)
        {
            FilmId = filmId;
            MediumType = mediumType;
        }

        public override string SurrogateKey()
        {
            return _keyService.ConstructMediumSurrogateKey(FilmId, MediumType);
        }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
    }
}
