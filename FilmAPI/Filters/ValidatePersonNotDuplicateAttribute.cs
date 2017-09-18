using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidatePersonNotDuplicateAttribute : TypeFilterAttribute
    {
        public ValidatePersonNotDuplicateAttribute() : base(typeof(ValidatePersonNotDuplicateFilterImpl))
        {
        }
        private class ValidatePersonNotDuplicateFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonRepository _personRepository;
            private readonly IKeyService _keyService;

            public ValidatePersonNotDuplicateFilterImpl(IPersonRepository repo, IKeyService service)
            {
                _personRepository = repo;
                _keyService = service;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (PersonViewModel)context.ActionArguments["model"];
                    if (model != null)
                    {
                        if ((await _personRepository.ListAsync()).Any(p => p.LastName == model.LastName && p.BirthdateString == model.BirthdateString))
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
