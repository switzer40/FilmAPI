using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BasePersonDto : IBaseDto
    {
        public BasePersonDto()
        {
        }
        public BasePersonDto(string lastName,
                             string birthdate,
                             string firstMidName = "")
        {
            LastName = lastName;
            Birthdate = birthdate;
            FirstMidName = firstMidName;
        }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public virtual void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(BasePersonDto))
            {
                var that = (BasePersonDto)dto;
                FirstMidName = that.FirstMidName;
                LastName = that.LastName;
                Birthdate = that.Birthdate;
            }
        }
        public virtual bool Equals(IBaseDto dto)
        {
            var result = false;
            if (dto.GetType() == typeof(BasePersonDto))
            {
                var that = (BasePersonDto)dto;
                result = FirstMidName.Equals(that.FirstMidName) &&
                         LastName.Equals(that.LastName) &&
                         Birthdate.Equals(that.Birthdate);
            }
            return result;
        }

    }
}
