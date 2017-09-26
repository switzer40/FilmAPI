using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidatePersonExistsAttribute :  TypeFilterAttribute
    {
        public ValidatePersonExistsAttribute() : base(typeof(ValidatePersonExistsFilterImpl))
        {
        }
        private class ValidatePersonExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonRepository _repository;
            private readonly IKeyService _keyService;
            public ValidatePersonExistsFilterImpl(IPersonRepository repo)
            {
                _repository = repo;
                _keyService = new KeyService();
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    if (string.IsNullOrEmpty(key))
                    {
                        context.Result = new BadRequestObjectResult("null key");
                        return;
                    }
                    (string lastName, string birthdate) = _keyService.DeconstructPesonSurrogateKey(key);
                    if (lastName == FilmConstants.BADKEY)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                    var p = _repository.GetByLastNameAndBirthdate(lastName, birthdate);
                    if (p == null)
                    {
                        context.Result = new NotFoundObjectResult(p.FullName);
                    }
                }
                await next();
            }
        }
    }
}
