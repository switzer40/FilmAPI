using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public abstract class BaseMapper<T> : IMapper<T> where T : BaseEntity
    {
        public abstract IBaseDto Map(T t);


        public abstract T MapBack(IBaseDto dto);


        public List<T> MapBackList(List<IBaseDto> list)
        {
            var result = new List<T>();
            foreach (var t in list)
            {
                result.Add(MapBack(t));
            }
            return result;
        }

        public List<IBaseDto> MapList(List<T> list)
        {
            var result = new List<IBaseDto>();
            foreach (var t in list)
            {
                result.Add(Map(t));

            }
            return result;
        }
    }
}
