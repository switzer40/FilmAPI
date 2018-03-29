using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
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
    public class ValidateMediumToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateMediumToUpdateExistsAttribute() : base(typeof(ValidateMediumToUpdateExistsFilterImpl))
        {
        }
        private class ValidateMediumToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            public ValidateMediumToUpdateExistsFilterImpl(IMediumRepository repo)
            {
                _repository = repo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                OperationStatus stat = OperationStatus.NotFound;
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BaseMediumDto)context.ActionArguments["model"];
                    var m = _repository.GetByTitleYearAndMediumType(model.Title, model.Year, model.MediumType).value;
                    if (m == null)
                    {
                        stat.ReasonForFailure = $"A medium for film {model.Title} does not exist";
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
