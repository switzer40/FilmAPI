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
    public class ValidateMediumNotDuplicateAttribute : TypeFilterAttribute
    {
        public ValidateMediumNotDuplicateAttribute() : base(typeof(ValidateMediumNotDuplicateFilterImpl))
        {
        }
        private class ValidateMediumNotDuplicateFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            public ValidateMediumNotDuplicateFilterImpl(IMediumRepository repo)
            {
                _repository = repo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BaseMediumDto)context.ActionArguments["model"];
                    var m = _repository.GetByTitleYearAndMediumType(model.Title, model.Year, model.MediumType).value;
                    if (m != null)
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
