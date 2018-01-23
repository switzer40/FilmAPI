using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BaseFilmDto : IBaseDto
    {
        public BaseFilmDto()
        {
        }
        public BaseFilmDto(string title, short year, short length = 10)
        {
            Title = title;
            Year = year;
            Length = length;
        }
        public string Title { get; set; }
        public short Year { get; set; }
        public short Length { get; set; }
        public virtual void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(BaseFilmDto))
            {
                var that = (BaseFilmDto)dto;
                Title = that.Title;
                Year = that.Year;
                Length = that.Length;
            }
        }
        public virtual bool Equals(IBaseDto dto)
        {
            var result = false;
            if (dto.GetType() == typeof(BaseFilmDto))
            {
                var that = (BaseFilmDto)dto;
                result = Title.Equals(that.Title) &&
                          Year.Equals(that.Year) &&
                          Length.Equals(that.Length);
            }
            return result;
        }
    }
}
