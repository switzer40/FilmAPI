using FilmAPI.Common.DTOs;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateFilmPersonNotDuplicateAttribute: TypeFilterAttribute
    {
        public ValidateFilmPersonNotDuplicateAttribute() : base(typeof(ValidateFilmPersonNotDuplicateFilterImpl))
        {
        }
        private class ValidateFilmPersonNotDuplicateFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            
            public ValidateFilmPersonNotDuplicateFilterImpl(IFilmPersonRepository repo)
            {
                _repository = repo;                
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BaseFilmPersonDto)context.ActionArguments["model"];
                    var fp = _repository.GetByTitleYearLastNameBirthdateAndRole(model.Title,
                                                                                model.Year,
                                                                                model.LastName,
                                                                                model.Birthdate,
                                                                                model.Role).value;
                    if (fp != null)
                    {
                        context.Result = new BadRequestObjectResult("Duplicate");
                        return;
                    }
                }
                await next();
            }
        }
    }
}
