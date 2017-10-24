using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IHomebrewMapper<EntityType, ModelType>
    {
        ModelType Map(EntityType e);
        EntityType MapBack(ModelType m);
        List<ModelType> MapList(List<EntityType> list);
            List<EntityType> MapBackList(List<ModelType> list);
    }
}
