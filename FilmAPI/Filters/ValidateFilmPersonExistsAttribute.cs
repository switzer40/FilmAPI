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
            public ValidateFilmPersonExistsFilterImpl(IFilmPersonRepository repository, IKeyService keyService)
            {
                _repository = repository;
                _keyService = keyService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    if (key != "")
                    {
                        _keyService.DeconstructFilmPersonSurrogateKey(key);
                        int filmId = _keyService.FilmPersonFilmId;
                        int personId = _keyService.FilmPersonPersonId;
                        string role = _keyService.FilmPersonRole;
                        FilmPerson fp = _repository.GetByFilmIdPersonIdAndRole(filmId, personId, role);
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
