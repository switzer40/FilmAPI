using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using System.Threading.Tasks;
using AutoMapper;
using FilmAPI.Core.Interfaces;

namespace FilmAPI.Services
{
    public class MediumService : EntityService<Medium, MediumViewModel>, IMediumService
    {        
        public MediumService(IRepository<Medium> repository, IMediumMapper mapper, IKeyService keyService) : base(repository, mapper, keyService)
        {
        }

        public override MediumViewModel GetBySurrogateKey(string key)
        {
            Film f = new Film(_keyService.FilmTitle, _keyService.FilmYear);
            return new MediumViewModel(f, _keyService.MediumMediumType, key);
        }

        public override async Task<MediumViewModel> GetBySurrogateKeyAsync(string key)
        {
            return await Task.Run<MediumViewModel>(() => GetBySurrogateKey(key));
        }
    }
}
