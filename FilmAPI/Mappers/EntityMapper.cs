using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Mappers
{
    public abstract class EntityMapper<EntityType, ModelType> : IEntityMapper<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {
        public abstract ModelType Map(EntityType e);


        public abstract EntityType MapBack(ModelType m);
        

        public List<ModelType> MapList(List<EntityType> list)
        {
            List<ModelType> result = new List<ModelType>();
            foreach (var entity in list)
            {
                result.Add(Map(entity));
            };
            return result;
        }

        public List<EntityType> MapListBack(List<ModelType> list)
        {
            List<EntityType> result = new List<EntityType>();
            foreach (var model in list)
            {
                result.Add(MapBack(model));
            };
            return result;
        }
    }
}
