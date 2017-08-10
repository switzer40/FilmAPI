using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public class MediumViewModel : BaseViewModel
    {
        public MediumViewModel(int filmId, string mediumType, string location = "")
        {
            FilmId = filmId;
            MediumType = mediumType;
            Location = location;
        }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
    }
}

