using FilmAPI.Core.Entities;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateMediumExistsAttribute : TypeFilterAttribute
    {
        public ValidateMediumExistsAttribute() : base(typeof(ValidateMediumExistsFilterImpl))
        {
        }
        private class ValidateMediumExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            private readonly IFilmRepository _filmRepository;
            private readonly IKeyService _keyService;
            public ValidateMediumExistsFilterImpl(IMediumRepository repository, IKeyService keyService, IFilmRepository frepo)
            {
                _repository = repository;
                _keyService = keyService;
                _filmRepository = frepo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    if (key != "")
                    {
                        (string title, short year, string mediumType) = _keyService.DeconstructMediumSurrogateKey(key);
                        if (title == FilmConstants.BADKEY)
                        {
                            context.Result = new BadRequestObjectResult(key);
                            return;
                        }
                        Film f = _filmRepository.GetByTitleAndYear(title, year);
                        if (f == null)
                        {
                            context.Result = new NotFoundObjectResult(key);
                            return;
                        }
                        Medium m = _repository.GetByFilmIdAndMediumType(f.Id, mediumType);
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
