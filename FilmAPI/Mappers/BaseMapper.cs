using FilmAPI.Interfaces;
using System.Collections.Generic;

namespace FilmAPI.Mappers
{
    public abstract class BaseMapper<EntityType, ModelType> : IHomebrewMapper<EntityType, ModelType>
    {
        public abstract ModelType Map(EntityType e);


        public abstract EntityType MapBack(ModelType m);
      

        public List<EntityType> MapBackList(List<ModelType> list)
        {
            List<EntityType> result = new List<EntityType>();
            foreach (var item in list)
            {
                result.Add(MapBack(item));
            }
            return result;
        }

        public List<ModelType> MapList(List<EntityType> list)
        {
            List<ModelType> result = new List<ModelType>();
            foreach (var item in list)
            {
                result.Add(Map(item));
            }
            return result;
        }
    }
}
