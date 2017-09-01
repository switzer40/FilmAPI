using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
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
    public class ValidatePersonExistsAttribute : TypeFilterAttribute
    {
        public ValidatePersonExistsAttribute() : base(typeof(ValidatePersonExistsFilterImpl))
        {
        }
        private class ValidatePersonExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonRepository _repository;
            private readonly IKeyService _keyService;
            public ValidatePersonExistsFilterImpl(IPersonRepository repository, IKeyService keyService)
            {
                _repository = repository;
                _keyService = keyService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    if (key != "")
                    {
                        _keyService.DeconstructPesonSurrogateKey(key);
                        string lastName = _keyService.PersonLastName;
                        string birthdate = _keyService.PersonBirthdate;
                        Person p = _repository.GetByLastNameAndBirthdate(lastName, birthdate);
                        if (p == null)
                        {
                            context.Result = new NotFoundObjectResult(key);
                            return;
                        }
                    }                    
                }
                await next();
            }
        }
    }
}
