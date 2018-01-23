using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{

    public class KeyedPersonDto : BasePersonDto, IKeyedDto
    {

        public KeyedPersonDto(string lastName,
                              string birthdate,
                              string firstMidName = "",
                              string key = "") : base(lastName, birthdate, firstMidName)
        {
            _key = key;
        }
        private string _key;
        public string Key { get => _key; set => _key = value; }
        public override void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(KeyedPersonDto))
            {
                var that = (KeyedPersonDto)dto;
                var b = that.Restrict();
                base.Copy(b);
                Key = that.Key;
            }
        }
        public override bool Equals(IBaseDto dto)
        {
            var result = base.Equals(dto);
            if (result && (dto.GetType() == typeof(KeyedPersonDto)))
            {
                var that = (KeyedPersonDto)dto;
                result = Key.Equals(that.Key);
            }
            return result;
        }

        public IBaseDto Restrict()
        {
            return new BasePersonDto(LastName, Birthdate, FirstMidName);
        }
    }
}
