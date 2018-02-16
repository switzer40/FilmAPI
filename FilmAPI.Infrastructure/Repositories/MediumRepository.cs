using FilmAPI.Common.Utilities;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.Specifications;
using FilmAPI.Infrastructure.Data;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmAPI.Infrastructure.Repositories
{
    public class MediumRepository: Repository<Medium>, IMediumRepository
    {
        private readonly IFilmRepository _filmRepository;
        public MediumRepository(FilmContext context,
                                IFilmRepository frepo) : base(context)    
        {
            _filmRepository = frepo;
        }

        public override OperationStatus Delete(string key)
        {
            var res = GetByKey(key);
            if (res.status != OperationStatus.OK)
            {
                return res.status;
            }
            return Delete(res.value);
        }

        public (OperationStatus status, Medium value) GetByFilmIdAndMediumType(int filmId, string mediumType)
        {
            var val = new Medium(filmId, mediumType);
            var status = OperationStatus.OK;
            var data = _filmRepository.GetById(filmId);
            if (data.status == OperationStatus.OK)
            {
                ISpecification<Medium> spec = new MediumByFilmIdAndMediumType(filmId, mediumType);
                var data1 = List(spec);
                val = data1.value.SingleOrDefault();                
            }
            else
            {
                val = null;
                status = data.status;                
            }
            return (status, val);
        }

        public async System.Threading.Tasks.Task<(OperationStatus status, Medium value)> GetByFilmIdAndMediumTypeAsync(int filmId, string mediumType)
        {
            return await Task.Run(() => GetByFilmIdAndMediumType(filmId, mediumType));
        }

        public (OperationStatus status, Medium value) GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public (OperationStatus status, Medium value) GetByTitleYearAndMediumType(string title, short year, string mediumType)
        {
            throw new NotImplementedException();
        }
    }
}
