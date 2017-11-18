using FilmAPI.Core.Entities;
using FilmAPI.Interfaces.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.DTOs;

namespace FilmAPI.Mappers
{
    public class PersonMapper : BaseMapper<Person>, IPersonMapper
    {
        public override IBaseDto<Person> Map(Person t)
        {
            var result = new BasePersonDto(t.LastName, t.BirthdateString, t.FirstMidName);
            return (IBaseDto<Person>)result;
        }

        public override Person MapBack(IBaseDto<Person> dto)            
        {
            var b = (BasePersonDto)dto;
            return new Person(b.LastName, b.Birthdate, b.FirstMidName);
        }
    }
}
