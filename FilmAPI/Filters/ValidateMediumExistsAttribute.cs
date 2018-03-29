using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
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
                OperationStatus stat = OperationStatus.OK;
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    var (title, year, mediumType) = _keyService.DeconstructMediumKey(key);
                    if (title == FilmConstants.BADKEY)
                    {
                        stat = OperationStatus.BadRequest;
                        stat.ReasonForFailure = $"Malformed key {key}";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                    else
                    {
                        var m = ((IMediumRepository)_repository).GetByTitleYearAndMediumType(title,
                                                                                             year,
                                                                                             mediumType).value;
                        if (m == null)
                        {
                            stat = OperationStatus.NotFound;
                            stat.ReasonForFailure = $"A medium with key {key} does not exist";
                            context.Result = new JsonResult(GetResult(stat));
                            return;
                        }
                    }
                    var f = _filmRepository.GetByTitleAndYear(title, year).value;
                    if (f == null)
                    {
                        stat =OperationStatus.NotFound;
                        stat.ReasonForFailure = $"A film {title} is missing";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                }
                await next();
            }
            private OperationResult<IKeyedDto> GetResult(OperationStatus stat)
            {
                IKeyedDto val = default;
                return new OperationResult<IKeyedDto>(stat, val);
            }
        }
    }
}
