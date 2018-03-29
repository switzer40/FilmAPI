using FilmAPI.Common.DTOs;
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
    public class ValidateFilmPersonToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmPersonToUpdateExistsAttribute() : base(typeof(ValidateFilmPersonToUpdateExistsFilterImpl))
        {
        }
        private class ValidateFilmPersonToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            public ValidateFilmPersonToUpdateExistsFilterImpl(IFilmPersonRepository repo)
            {
                _repository = repo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                OperationStatus stat = OperationStatus.NotFound;
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BaseFilmPersonDto)context.ActionArguments["model"];
                    var fp = _repository.GetByTitleYearLastNameBirthdateAndRole(model.Title,
                                                                                model.Year,
                                                                                model.LastName,
                                                                                model.Birthdate,
                                                                                model.Role).value;
                    if (fp == null)
                    {
                        stat.ReasonForFailure = $"A relation between film {model.Title}  and person {model.LastName} does not exist";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                }
                await next();
            }
            OperationResult<IKeyedDto> GetResult(OperationStatus stat)
            {
                IKeyedDto val = default;
                return new OperationResult<IKeyedDto>(stat, val);
            }
        }
    }

    
}
