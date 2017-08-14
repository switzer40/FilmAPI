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
        public Film(string title, short year, short length = 0)
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
