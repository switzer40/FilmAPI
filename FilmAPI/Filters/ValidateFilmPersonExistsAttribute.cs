using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
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
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    var data = _keyService.DeconstructFilmPersonKey(key);
                    if (data.title == FilmConstants.BADKEY)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                    else
                    {
                        var fp = ((IFilmPersonRepository)_repository).GetByTitleYearLastNameBirthdateAndRole(data.title,
                                                                                                             data.year,
                                                                                                             data.lastName,
                                                                                                             data.birthdate,
                                                                                                             data.role).value;
                        if (fp == null)
                        {
                            context.Result = new NotFoundObjectResult(key);
                            return;
                        }
                    }
                    var f = _filmRepository.GetByTitleAndYear(data.title, data.year).value;
                    if (f == null)
                    {
                        context.Result = new BadRequestObjectResult("Missing film");
                        return;
                    }
                    var p = _personRepository.GetByLastNameAndBirthdate(data.lastName, data.birthdate).value;
                    if (p == null)
                    {
                        context.Result = new BadRequestObjectResult("Missing person");
                        return;
                    }
                }
                await next();
            }
        }
    }
}
