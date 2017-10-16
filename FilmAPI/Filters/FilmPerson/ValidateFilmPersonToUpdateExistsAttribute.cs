using FilmAPI.Core.Interfaces;
using FilmAPI.DTOs.FilmPerson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters.FilmPerson
{
    public class ValidateFilmPersonToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmPersonToUpdateExistsAttribute() : base(typeof(ValidateFilmPersonToUpdateExistsFilterImpl))
        {
        }
        private class ValidateFilmPersonToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            public ValidateFilmPersonToUpdateExistsFilterImpl(IFilmPersonRepository repo,
                                                              IFilmRepository frepo,
                                                              IPersonRepository prepo)
            {
                _repository = repo;
                _filmRepository = frepo;
                _personRepository = prepo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("model"))
                {
                    var model = (BaseFilmPersonDto)context.ActionArguments["model"];
                    var f = _filmRepository.GetByTitleAndYear(model.Title, model.Year);
                    var p = _personRepository.GetByLastNameAndBirthdate(model.LastName, model.Birthdate);
                    var msg = "The foööowing entities are missing in the DB: ";
                    if ((f == null) && (p == null))
                    {
                        if (f == null)
                        {
                            msg += $"film '{model.Title}'";
                        }
                        else if (p == null)
                        {
                            msg += $"person {model.LastName}";
                        }
                        context.Result = new NotFoundObjectResult(msg);
                        return;
                    }
                    var fp = _repository.GetByFilmIdPersonIdAndRole(f.Id, p.Id, model.Role);
                    if (fp == null)
                    {
                        msg += $"the relation of {p.FullName} to film'{f.Title}'";
                        context.Result = new NotFoundObjectResult(msg);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
