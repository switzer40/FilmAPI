using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Interfaces
{
    public interface IKeyedDto : IBaseDto
    {
        string Key { get; set; }
        IBaseDto Restrict()
;
    }
}
