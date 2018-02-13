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
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BasePersonDto)context.ActionArguments["model"];
                    var p = _repository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate).value;
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
