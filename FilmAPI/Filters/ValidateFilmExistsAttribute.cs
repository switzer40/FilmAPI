using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateFilmExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmExistsAttribute() : base(typeof(ValidateFilmExistsFilterLmpl))
        {
        }
        private class ValidateFilmExistsFilterLmpl : IAsyncActionFilter
        {
            private readonly IFilmRepository _repository;
            private readonly IKeyService _keyService;
            public ValidateFilmExistsFilterLmpl(IFilmRepository repository, IKeyService keyService)
            {
                _repository = repository;
                _keyService = keyService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string) context.ActionArguments["key"];
                    if (key != "")
                    {
                        (string title, short year) = _keyService.DeconstructFilmSurrogateKey(key);                        
                        Film f = _repository.GetByTitleAndYear(title, year);
                        if (f == null)
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
