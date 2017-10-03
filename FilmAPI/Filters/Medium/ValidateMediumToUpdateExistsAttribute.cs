using FilmAPI.Core.Interfaces;
using FilmAPI.DTOs.Medium;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters.Medium
{
    public class ValidateMediumToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateMediumToUpdateExistsAttribute() : base(typeof(ValidateMediumToUpdateExistFilterImpl))
        {
        }
        private class ValidateMediumToUpdateExistFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            public ValidateMediumToUpdateExistFilterImpl(IMediumRepository repo)
            {
                _repository = repo ?? throw new ArgumentNullException(nameof(repo));
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (KeyedMediumDto)context.ActionArguments["model"];
                    if (model != null)
                    {
                        var m = _repository.GetByTitleYearAndMediumType(model.Title, model.Year, model.MediumType);
                        if (m == null)
                        {
                            context.Result = new NotFoundObjectResult(model);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
