using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;

namespace FilmAPI.Services
{
    public class MediumService : EntityService<Medium, MediumViewModel>, IMediumService
    {        
        public MediumService(IRepository<Medium> repository, IMapper mapper, IKeyService keyService) : base(repository, mapper, keyService)
        {
        }

        public override MediumViewModel GetBySurrogateKey(string key)
        {
            _keyService.DeconstructMedumSurrogateKey(key);
            return new MediumViewModel(_keyService,  _keyService.MediumFilmId, _keyService.MediumMediumType);
        }

        public override async Task<MediumViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run(() => GetBySurrogateKey(key));
        }
    }
}
