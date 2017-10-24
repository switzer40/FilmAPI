using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace FilmAPI.Filters.Person
{
    public class ValidatePersonToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidatePersonToUpdateExistsAttribute() : base(typeof(ValidatePersonToUpdateExistsFilterImpl))
        {
        }
        private class ValidatePersonToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonRepository _repository;
            public ValidatePersonToUpdateExistsFilterImpl(IPersonRepository repo)
            {
                _repository = repo;
            }
            public async Task OnActionExecutingAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (KeyedPersonDto)context.ActionArguments["model"];
                    var p = _repository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                    if (p == null)
                    {
                        context.Result = new NotFoundObjectResult(model);
                        return;
                    }
                }
                await next();
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if(context.ActionArguments.ContainsKey("model"))
                {
                    var model = (KeyedPersonDto)context.ActionArguments["model"];
                    var p = _repository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                    if (p == null)
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
