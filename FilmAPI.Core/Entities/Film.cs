using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Core.Entities
{
    public class Film : BaseEntity
    {
        public Film()
        {
        }
        public Film(string title, short year, short length = 10)
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

        public override void Copy(BaseEntity e)
        {
            if (e.GetType() == typeof(Film))
            {
                var that = (Film)e;
                Title = that.Title;
                Year = that.Year;
                Length = that.Length;
            }
        }
    }
}
