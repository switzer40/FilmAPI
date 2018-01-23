using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Interfaces
{
    public interface IBaseDto
    {
        void Copy(IBaseDto dto);
        bool Equals(IBaseDto dto);
    }
}
