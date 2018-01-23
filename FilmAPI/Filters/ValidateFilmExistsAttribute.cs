using FilmAPI.Common.Constants;
using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
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
                    var key = (string)context.ActionArguments["key"];
                    var data = _keyService.DeconstructFilmKey(key);
                    if (data.title == FilmConstants.BADKEY)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                    else
                    {
                        var f = ((IFilmRepository)_repository).GetByTitleAndYear(data.title, data.year);
                        if (f == null)
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