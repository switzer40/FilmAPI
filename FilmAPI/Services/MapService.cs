using FilmAPI.Interfaces;
using System.Collections.Generic;
using FilmAPI.ViewModels;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;

namespace FilmAPI.Services
{
    public abstract class MapService<EntityType, ModelType> : IMapService<EntityType, ModelType>
        where EntityType : BaseEntity
        where ModelType : BaseViewModel
    {

        public MapService(IFilmRepository frepo, IPersonRepository prepo)
        {
        }

        public abstract ModelType Map(EntityType e);


        public abstract EntityType MapBack(ModelType m);
       

        public List<ModelType> MapList(List<EntityType> list)
        {
            List<ModelType> result = new List<ModelType>();
            foreach (var e in list)
            {

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
