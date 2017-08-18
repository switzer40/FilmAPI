using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.VviewModls
{
    public class MediumViewModel : BaseViewModel
    {
        public MediumViewModel(Film f, string mediumType)
        {
            FilmTitle = f.Title;
            FilmYear = f.Year;
            MediumType = mediumType;
        }
        [Required]
        public string FilmTitle { get; set; }
        [Required]
        public short FilmYear { get; set; }
        [Required]
        public string MediumType { get; set; }
    }
}
