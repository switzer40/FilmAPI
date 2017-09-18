using FilmAPI.Core.Interfaces;
using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateFilmNotDuplicateAttribute : TypeFilterAttribute
    {
        public ValidateFilmNotDuplicateAttribute() : base(typeof(ValidateFilmNotDuplicateFilterImpl))
        {
        }
        private class ValidateFilmNotDuplicateFilterImpl : IAsyncActionFilter
        {
            private IFilmRepository _filmRepository;
            private IKeyService _keyService;

            public ValidateFilmNotDuplicateFilterImpl(IFilmRepository filmRepository, IKeyService keyService)
            {
                _filmRepository = filmRepository;
                _keyService = keyService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (FilmViewModel)context.ActionArguments["model"];
                    if (model != null)
                    {                        
                        if ((await _filmRepository.ListAsync()).Any (f => f.Title == model.Title && f.Year == model.Year))
                        {
                            context.Result =new  BadRequestObjectResult("duplicate");
                            return;
                        }
                    }
                }
                await next();
            }
        }

    }
}
