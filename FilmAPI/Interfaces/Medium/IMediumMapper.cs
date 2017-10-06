using FilmAPI.DTOs.Medium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Medium
{
    public interface IMediumMapper : IHomebrewMapper<Core.Entities.Medium, BaseMediumDto>
    {
        Core.Entities.Medium MapBackForce(BaseMediumDto b);        
    }
}
