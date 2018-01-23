using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BaseFilmPersonDto : IBaseDto
    {
        public BaseFilmPersonDto()
        {
        }
        public BaseFilmPersonDto(string title,
                                 short year,
                                 string lastName,
                                 string birthdate,
                                 string role)
        {
            Title = title;
            Year = year;
            LastName = lastName;
            Birthdate = birthdate;
            Role = role;
        }
        public string Title { get; set; }
        public short Year { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Role { get; set; }

        public virtual void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(BaseFilmPersonDto))
            {
                var that = (BaseFilmPersonDto)dto;
                Title = that.Title;
                Year = that.Year;
                LastName = that.LastName;
                Birthdate = that.Birthdate;
                Role = that.Role;
            }
        }
        public virtual bool Equals(IBaseDto dto)
        {
            var result = false;
            if (dto.GetType() == typeof(BaseFilmPersonDto))
            {
                var that = (BaseFilmPersonDto)dto;
                result = Title.Equals(that.Title) &&
                         Year.Equals(that.Year) &&
                         LastName.Equals(that.LastName) &&
                         Birthdate.Equals(that.Birthdate) &&
                         Role.Equals(that.Role);
            }
            return result;
        }
    }
}
