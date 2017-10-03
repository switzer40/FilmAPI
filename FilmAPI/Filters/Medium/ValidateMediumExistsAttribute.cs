using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters.Medium
{
    public class ValidateMediumExistsAttribute : TypeFilterAttribute
    {
        public ValidateMediumExistsAttribute() : base(typeof(ValidateMediumExistFilterImpl))
        {
        }
        private class ValidateMediumExistFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            private readonly IKeyService _keyService;
            public ValidateMediumExistFilterImpl(IMediumRepository repository, IKeyService keyService)
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
                        var data = _keyService.DeconstructMediumSurrogateKey(key);
                        if (data.title == FilmConstants.BADKEY)
                        {
                            context.Result = new BadRequestObjectResult(key);
                            return;
                        }
                        var m = _repository.GetByTitleYearAndMediumType(data.title, data.year, data.mediumType);
                        if (m == null)
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
