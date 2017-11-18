using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Interfaces
{
    public interface IBaseDto<T> where T : BaseEntity
    {
    }
}
