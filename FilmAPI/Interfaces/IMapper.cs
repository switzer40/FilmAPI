using FilmAPI.Common.Interfaces;
using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IMapper<T> where T : BaseEntity
    {
        IBaseDto Map(T t);
        T MapBack(IBaseDto dto);
        List<IBaseDto> MapList(List<T> list);
        List<T> MapBackList(List<IBaseDto> list);
    }
}