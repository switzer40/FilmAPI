using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Mappers
{
    public interface IMapper<T> where T : BaseEntity
    {
        IBaseDto<T> Map(T t);
        T MapBack(IBaseDto<T> b);
        List<IBaseDto<T>> MapList(List<T> list);
        List<T> MapBackList(List<IBaseDto<T>> list);
    }
}
