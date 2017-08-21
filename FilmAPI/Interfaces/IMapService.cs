using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Mappers;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IMapService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        ModelType Map(EntityType e);
        EntityType MapBack(ModelType m);
        List<ModelType> MapList(List<EntityType> list);
        List<EntityType> MapListBack(List<ModelType> list);
    }
}
