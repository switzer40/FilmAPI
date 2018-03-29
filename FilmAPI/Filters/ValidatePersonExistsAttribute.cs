using FilmAPI.Common.Constants;
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
    public class ValidatePersonExistsAttribute : TypeFilterAttribute
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
                OperationStatus stat = OperationStatus.OK;
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    var (lastName, birthdate) = _keyService.DeconstructPersonKey(key);
                    if (lastName == FilmConstants.BADKEY)
                    {
                        stat = OperationStatus.BadRequest;
                        stat.ReasonForFailure = $"Malformedkey {key}";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                    else
                    {
                        var p = ((IPersonRepository)_repository).GetByLastNameAndBirthdate(lastName, birthdate).value;
                        if (p == null)
                        {
                            stat = OperationStatus.NotFound;
                            stat.ReasonForFailure = $"A person {lastName} does not exist.";
                            context.Result = new JsonResult(GetResult(stat));
                            return;
                        }
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
