using FilmAPI.Core.Interfaces;
using FilmAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateMediumToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateMediumToUpdateExistsAttribute() :base(typeof(ValidateMediumToUpdateExistsFilterImpl))
        {
        }
        private class ValidateMediumToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IMediumRepository _repository;
            private readonly IFilmRepository _filmRepository;
            public ValidateMediumToUpdateExistsFilterImpl(IMediumRepository repo, IFilmRepository frepo)
            {
                _repository = repo;
                _filmRepository = frepo;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (MediumViewModel) context.ActionArguments["model"];
                    var f = _filmRepository.GetByTitleAndYear(model.FilmTitle, model.FilmYear);
                    var found = (f != null);
                    if (found)
                    {
                        var m = _repository.GetByFilmIdAndMediumType(f.Id, model.MediumType);
                        found = (m != null);
                    }
                    if (!found)
                    {
                        context.Result = new NotFoundObjectResult(model);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
