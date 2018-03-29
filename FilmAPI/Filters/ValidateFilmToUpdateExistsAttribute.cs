﻿using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
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
    public class ValidateFilmToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmToUpdateExistsAttribute(): base(typeof(ValidateFilmToUpdateExistsFilterImpl))
       {
        }
        private class ValidateFilmToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmRepository _repository;
            public ValidateFilmToUpdateExistsFilterImpl(IFilmRepository repo)
            {
                _repository = repo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    OperationStatus stat = OperationStatus.NotFound;
                    var model = (BaseFilmDto)context.ActionArguments["model"];
                    var f = _repository.GetByTitleAndYear(model.Title, model.Year).value;
                    if (f == null)
                    {
                        stat.ReasonForFailure = $"A film {model.Title} does not exist.";
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
