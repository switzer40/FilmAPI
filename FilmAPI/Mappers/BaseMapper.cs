using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;

namespace FilmAPI.Mappers
{
    public abstract class BaseMapper<T> : IMapper<T> where T : BaseEntity
    {
        public abstract IBaseDto<T> Map(T t);


        public abstract T MapBack(IBaseDto<T> b);
        

        public List<T> MapBackList(List<IBaseDto<T>> list)
        {
            var result = new List<T>();
            foreach (var b in list)
            {
                result.Add(MapBack(b));
            }
            return result;
        }

        public List<IBaseDto<T>> MapList(List<T> list)
        {
            var result = new List<IBaseDto<T>>();
            foreach (var t in list)
            {
                result.Add(Map(t));
            }
            return result;
        }
    }
}
