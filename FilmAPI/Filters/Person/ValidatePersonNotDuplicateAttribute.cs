using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace FilmAPI.Filters.Person
{
    public class ValidatePersonNotDuplicateAttribute : TypeFilterAttribute
    {
        public ValidatePersonNotDuplicateAttribute() : base(typeof(ValidatePersonNotDuplicateFilterImpl))
        {
        }
        private class ValidatePersonNotDuplicateFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonRepository _repository;
            public ValidatePersonNotDuplicateFilterImpl(IPersonRepository repo)
            {
                _repository = repo;
            }
            public async Task OnActionExecutingAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BasePersonDto)context.ActionArguments["model"];
                    var p = _repository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                    if (p != null)
                    {
                        context.Result = new BadRequestObjectResult(model);
                        return;
                    }
                }
                await next();
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BasePersonDto)context.ActionArguments["model"];
                    var p = _repository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                    if (p != null)
                    {
                        context.Result = new BadRequestObjectResult(model);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
