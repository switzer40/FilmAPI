using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs.Medium
{
    public class BaseMediumDto
    {
        public BaseMediumDto(string title, short year, string mediumType, string location = "")
        {
            Title = title;
            Year = year;
            MediumType = mediumType;
            Location = location;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1850, 2050)]
        public short Year { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
    }
}