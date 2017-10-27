using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace FilmAPI.Filters.Medium
{
    public class ValidateMediumNotDuplicateAttribute : TypeFilterAttribute
    {
        public ValidateMediumNotDuplicateAttribute() :base(typeof(ValidateMediumNotDuplicateFilterImpl))
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
                    var model = (KeyedMediumDto)context.ActionArguments["model"];
                    if (model != null)
                    {
                        var m = _repository.GetByTitleYearAndMediumType(model.Title, model.Year, model.MediumType);
                        if (m != null)
                        {
                            context.Result = new BadRequestObjectResult("duplicate");
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
