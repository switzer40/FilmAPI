using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateFilmPersonExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmPersonExistsAttribute() : base(typeof(ValidateFilmPersonExistsFilterImpl))
        {
        }
        private class ValidateFilmPersonExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            private readonly IKeyService _keyService;
            public ValidateFilmPersonExistsFilterImpl(IFilmPersonRepository repo,
                                                      IFilmRepository frepo,
                                                      IPersonRepository prepo)
            {
                _repository = repo;
                _filmRepository = frepo;
                _personRepository = prepo;
                _keyService = new KeyService();
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                OperationStatus stat = OperationStatus.OK;
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    var (title, year, lastName, birthdate, role) = _keyService.DeconstructFilmPersonKey(key);
                    if (title == FilmConstants.BADKEY)
                    {
                        stat = OperationStatus.BadRequest;
                        stat.ReasonForFailure = $"Malformed key {key}";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                    else
                    {
                        var fp = ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirthdateAndRole(title,
                                                                                                             year,
                                                                                                             lastName,
                                                                                                             birthdate,
                                                                                                             role).value;
                        if (fp == null)
                        {
                            stat = OperationStatus.NotFound;
                            stat.ReasonForFailure = $"A relation with key {key} does not exist";
                            context.Result = new JsonResult(GetResult(stat));
                            return;
                        }
                    }
                    var f = _filmRepository.GetByTitleAndYear(title, year).value;
                    if (f == null)
                    {
                        stat = OperationStatus.BadRequest;
                        stat.ReasonForFailure = $"A film {title} is missing";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                    var p = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate).value;
                    if (p == null)
                    {
                        stat = OperationStatus.BadRequest;
                        stat.ReasonForFailure = $"A person {lastName} is missing";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                }
                await next();
            }
            private OperationResult<IKeyedDto> GetResult(OperationStatus stat)
            {
                IKeyedDto val = default;
                return new OperationResult<IKeyedDto>(stat, val);
            }
        }
    }
}
