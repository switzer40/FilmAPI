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
            Medium retVal = default;
            OperationStatus retStatus = OperationStatus.OK;
            var (title, year, mediumType) = _keyService.DeconstructMediumKey(key);
            var (status1, value) = _filmRepository.GetByTitleAndYear(title, year);
            if (status1 == OperationStatus.OK)
            {
                var spec = new MediumByFilmIdAndMediumType(value.Id, mediumType);
                var (status2, list) = List(spec);
                if (status2 == OperationStatus.OK)
                {
                    retVal = list.FirstOrDefault();
                }
                else
                {
                    retStatus = status2;
                }

            }
            else
            {
                retStatus = status1;
            }
            return (retStatus, retVal);            
        }

        public (OperationStatus status, Medium value) GetByTitleYearAndMediumType(string title, short year, string mediumType)
        {
            Medium retVal = default;
            OperationStatus retStat = OperationStatus.OK;
            var key = _keyService.ConstructMediumKey(title, year, mediumType);
            var (status, value) = GetByKey(key);
            if (status == OperationStatus.OK)
            {
                retVal = value;
            }
            else
            {
                retStat = status;
            }
            return (retStat, retVal);
        }

        public override OperationStatus Update(Medium t)
        {
            Medium storedMedium = default;
            var (status, value) = _filmRepository.GetById(t.FilmId);
            if (status == OperationStatus.OK)
            {
                Film f = value;
                var res = GetByTitleYearAndMediumType(f.Title, f.Year, t.MediumType);
                status = res.status;
                if (status == OperationStatus.OK)
                {
                    storedMedium = res.value;
                    storedMedium.Copy(t);
                    Save();
                }
            }
            return status;
        }
    }
}
