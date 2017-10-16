using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters.FilmPerson
{
    public class ValidateFilmPersonExistsAttribute : TypeFilterAttribute
    {
        public ValidateFilmPersonExistsAttribute() : base(typeof(ValidateFilmPersonExistsFilterImpl))
        {
        }
        private class ValidateFilmPersonExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmPersonRepository _repository;
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            private readonly IKeyService _keyService;
            public ValidateFilmPersonExistsFilterImpl(IFilmPersonRepository repo,
                                                       IFilmRepository frepo,
                                                       IPersonRepository prepo,
                                                       IKeyService keyService)
            {
                _repository = repo;
                _filmRepository = frepo;
                _personRepository = prepo;
                _keyService = keyService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var msg = "The following data may be incorrect: ";
                    var key = (string)context.ActionArguments["key"];
                    var data = _keyService.DeconstructFilmPersonSurrogateKey(key);
                    if (data.title == FilmConstants.BADKEY)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                    var f = _filmRepository.GetByTitleAndYear(data.title, data.year);
                    var p = _personRepository.GetByLastNameAndBirthdate(data.lastName, data.birthdate);
                    if ((f == null) || (p == null))
                    {
                        if (f == null)
                        {
                            msg += $"the film title '{data.title}' or the film year {data.year}";
                        }
                        if (p == null)
                        {
                            msg += $"the person's last name {data.lastName} or the person's birth date {data.birthdate}";
                        }
                        context.Result = new NotFoundObjectResult(msg);
                        return;
                    }
                    var fp = _repository.GetByFilmIdPersonIdAndRole(f.Id, p.Id, data.role);
                    if (fp == null)
                    {
                        msg += $"the role {data.role} in the relation of {p.FullName} to {f.Title}";
                        context.Result = new NotFoundObjectResult(msg);
                        return;
                    }
                }
                await next();
            }
        }
    }
}
