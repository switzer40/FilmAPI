using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public class PersonMapper : BaseMapper<Person>, IPersonMapper
    {
        public override IBaseDto Map(Person t)
        {
            return new BasePersonDto(t.LastName, t.BirthdateString, t.FirstMidName);
        }

        public override Person MapBack(IBaseDto dto)
        {
            var b = (BasePersonDto)dto;
            return new Person(b.LastName, b.Birthdate, b.FirstMidName);
        }
    }
}