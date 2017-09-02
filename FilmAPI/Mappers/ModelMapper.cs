using FilmAPI.Interfaces;
using System.Collections.Generic;
using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;

namespace FilmAPI.Mappers
{
    public abstract class ModelMapper<EntityType, ModelType> : IModelMapper<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        public abstract ModelType Map(EntityType e);


        public abstract EntityType MapBack(ModelType m);
       

        public List<ModelType> MapList(List<EntityType> list)
        {
            List<ModelType> result = new List<ModelType>();
            foreach (var e in list)
            {
                result.Add(Map(e));
            }
            return result;
        }

        public List<EntityType> MapListBack(List<ModelType> list)
        {
            List<EntityType> result = new List<EntityType>();
            foreach (var m in list)
            {
                result.Add(MapBack(m));
            }
            return result;
        }
    }
}
