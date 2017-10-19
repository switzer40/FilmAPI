using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters.Person
{
    public class ValidatePersonExistsAttribute : TypeFilterAttribute
    {
        public ValidatePersonExistsAttribute() : base(typeof(ValidatePersonExistsFilterImpl))
        {
        }
        private class ValidatePersonExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonRepository _repository;
            private readonly IKeyService _keyService;
            public ValidatePersonExistsFilterImpl(IPersonRepository repo, IKeyService keyService)
            {
                _repository = repo;
                _keyService = keyService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {                
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    var data = _keyService.DeconstructPersonKey(key);
                    if (data.lastName == FilmConstants.BADKEY)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                    var p = _repository.GetByLastNameAndBirthdate(data.lastName, data.birthdate);
                    if (p == null)
                    {
                        context.Result = new NotFoundObjectResult(key);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
