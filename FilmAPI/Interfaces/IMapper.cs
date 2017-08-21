using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IMapper<EntityType> where EntityType : BaseEntity
    {
    }
}
