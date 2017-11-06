using FilmAPI.Common.DTOs.Medium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces.Medium
{
    public interface IMediumService : IEntityService<Core.Entities.Medium, BaseMediumDto, KeyedMediumDto>
    {
    }
}
