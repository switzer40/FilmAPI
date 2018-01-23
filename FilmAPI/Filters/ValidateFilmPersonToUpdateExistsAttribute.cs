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
    public class ValidateFilmPersonToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmPersonToUpdateExistsAttribute() : base(typeof(ValidateFilmPersonToUpdateExistsFilterImpl))
        {
        }
        private class ValidateFilmPersonToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            public ValidateFilmPersonToUpdateExistsFilterImpl(IFilmPersonRepository repo)
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
                                                                                model.Role);
                    if (fp == null)
                    {
                        context.Result = new NotFoundObjectResult(model);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
