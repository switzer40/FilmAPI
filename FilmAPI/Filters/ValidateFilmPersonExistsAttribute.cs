using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
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
            private readonly IKeyService _keyService;
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            public ValidateFilmPersonExistsFilterImpl(IFilmPersonRepository repository,
                                                     IKeyService keyService,
                                                     IFilmRepository filmRepository,
                                                     IPersonRepository personRepository)
            {
                _repository = repository;
                _keyService = keyService;
                _filmRepository = filmRepository;
                _personRepository = personRepository;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    if (key != "")
                    {
                        (string title, short year, string lastName, string birthdate, string role) =
                            _keyService.DeconstructFilmPersonSurrogateKey(key);
                        _keyService.DeconstructFilmPersonSurrogateKey(key);
                        Film f = _filmRepository.GetByTitleAndYear(title, year);
                        Person p = _personRepository.GetByLastNameAndBirthdate(lastName, birthdate);
                        FilmPerson fp = _repository.GetByFilmIdPersonIdAndRole(f.Id, p.Id, role);
                        if (fp == null)
                        {
                            context.Result = new NotFoundObjectResult(key);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
