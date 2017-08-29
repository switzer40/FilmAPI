using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;
using System.Collections.Generic;

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
