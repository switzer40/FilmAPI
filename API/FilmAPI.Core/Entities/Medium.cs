using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Core.Entities
{
    public class Medium : BaseEntity
    {
        public Medium(int filmId, string type, string location = "")
        {
            FilmId = filmId;
            MediumType = type;
            Location = location;
        }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
    }
}
