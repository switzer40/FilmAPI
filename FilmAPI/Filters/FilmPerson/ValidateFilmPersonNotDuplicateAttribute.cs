using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs.FilmPerson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters.FilmPerson
{
    public class ValidateFilmPersonNotDuplicateAttribute : TypeFilterAttribute
    {
        public ValidateFilmPersonNotDuplicateAttribute() : base(typeof(ValidateFilmPersonNotDuplicateFilterImpl))
        {
        }
        private class ValidateFilmPersonNotDuplicateFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            private bool _force = false;
            public ValidateFilmPersonNotDuplicateFilterImpl(IFilmPersonRepository repo,
                                                            IFilmRepository frepo,
                                                            IPersonRepository prepo)
            {
                _repository = repo;
                _filmRepository = frepo;
                _personRepository = prepo;
                _force = FilmConstants.Force;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BaseFilmPersonDto) context.ActionArguments["model"];
                    var f = _filmRepository.GetByTitleAndYear(model.Title, model.Year);
                    if (f == null && _force)
                    {
                        f = new Core.Entities.Film(model.Title, model.Year);
                    }
                    var p = _personRepository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                    if (p == null && _force)
                    {
                        p = new Core.Entities.Person(model.LastName, model.Birthdate);
                    }
                    if (f == null)
                    {
                        context.Result = new NotFoundObjectResult(model.Title);
                        return;
                    }
                    if (p == null)
                    {
                        context.Result = new NotFoundObjectResult(model.LastName);
                        return;
                    }
                    if ((f != null) && (p != null))
                    {
                        var fp = _repository.GetByFilmIdPersonIdAndRole(f.Id, p.Id, model.Role);
                        if (fp != null)
                        {
                            context.Result = new BadRequestObjectResult("This would be a duplicate");
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
