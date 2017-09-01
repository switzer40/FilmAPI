using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Core.Entities
{
    public class Film : BaseEntity
    {
        private Film()
        {

        }
        public Film(string title, short year, short length = 10)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Title { get; set; }
        [Required]
        [Range(1850, 2100)]
        public short Year { get; set; }
        [Range(10, 200)]
        public short Length { get; set; }
    }
}
