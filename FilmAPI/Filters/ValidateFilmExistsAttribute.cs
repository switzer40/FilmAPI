using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateFilmExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmExistsAttribute() : base(typeof(ValidateFilmExistsFilterImpl))
        {
        }
        private class ValidateFilmExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmRepository _repository;
            private readonly IKeyService _keyService;
            public ValidateFilmExistsFilterImpl(IFilmRepository repo)
            {
                _repository = repo;
                _keyService = new KeyService();
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    OperationStatus stat = OperationStatus.OK;
                    var key = (string)context.ActionArguments["key"];
                    var data = _keyService.DeconstructFilmKey(key);
                    if (data.title == FilmConstants.BADKEY)
                    {
                        stat = OperationStatus.BadRequest;
                        stat.ReasonForFailure = $"Malformed key {key}";
                        context.Result = new JsonResult(GetResult(stat));
                        return;
                    }
                    else
                    {
                        var f = ((IFilmRepository)_repository).GetByTitleAndYear(data.title, data.year).value;
                        if (f == null)
                        {
                            stat = OperationStatus.NotFound;
                            stat.ReasonForFailure = $"A film {data.title} does not exist.";
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