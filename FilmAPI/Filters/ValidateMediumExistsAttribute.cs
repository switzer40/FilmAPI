using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateMediumExistsAttribute :TypeFilterAttribute
    {
        public ValidateMediumExistsAttribute() : base(typeof(ValidateMediumExistsFilterImpl))
        {
        }
        private class ValidateMediumExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            private readonly IFilmRepository _filmRepository;
            private readonly IKeyService _keyService;
            public ValidateMediumExistsFilterImpl(IMediumRepository repo, IFilmRepository frepo)
            {
                _repository = repo;
                _filmRepository = frepo;
                _keyService = new KeyService();
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    var data = _keyService.DeconstructMediumKey(key);
                    if (data.title == FilmConstants.BADKEY)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                    else
                    {
                        var m = ((IMediumRepository)_repository).GetByTitleYearAndMediumType(data.title,
                                                                                             data.year,
                                                                                             data.mediumType);
                        if (m == null)
                        {
                            context.Result = new NotFoundObjectResult(key);
                            return;
                        }
                    }
                    var f = _filmRepository.GetByTitleAndYear(data.title, data.year);
                    if (f == null)
                    {
                        context.Result = new BadRequestObjectResult("Missing film");
                        return;
                    }
                }
                await next();
            }
        }
    }
}
