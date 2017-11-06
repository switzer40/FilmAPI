using FilmAPI.Common.DTOs.Film;
using FilmAPI.Common.DTOs.FilmPerson;
using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.Entities;
using FilmAPI.Core.Exceptions;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Filters
{
    public class ValidateEntityToUpdateExistsAttribute : TypeFilterAttribute
    {
        public ValidateEntityToUpdateExistsAttribute() : base(typeof(ValidateEntityToUpdateExistsFilterImpl))
        {
        }
        private class ValidateEntityToUpdateExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            public ValidateEntityToUpdateExistsFilterImpl(IFilmRepository frepo,
                                                          IPersonRepository prepo)
            {
                _filmRepository = frepo;
                _personRepository = prepo;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("t"))
                {
                    BaseEntity model = null;
                    var arg = (BaseEntity)context.ActionArguments["t"];
                    try
                    {
                        model = ExtractEntity(context, arg);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    if (model == null)
                    {
                        context.Result = new BadRequestObjectResult(arg);
                        return;
                    }
                }
                await next();
            }

            private BaseEntity ExtractEntity(ActionExecutingContext context, object arg)
            {
                var controllerName = context.Controller.GetType().Name;
                switch (controllerName)
                {
                    case "FilmsController":
                        string title = ((BaseFilmDto)arg).Title;
                        short year = ((BaseFilmDto)arg).Year;
                        short length = ((BaseFilmDto)arg).Length;
                        return new Film(title, year, length);
                    case "PeopleController":
                        var lastName = ((BasePersonDto)arg).LastName;
                        var birthdate = ((BasePersonDto)arg).Birthdate;
                        var firstMidName = ((BasePersonDto)arg).FirstMidName;
                        return new Core.Entities.Person(lastName, birthdate, firstMidName);
                    case "MediaController":
                        var mtitle = ((BaseMediumDto)arg).Title;
                        var myear = ((BaseMediumDto)arg).Year;
                        var mediumType = ((BaseMediumDto)arg).MediumType;
                        var f = _filmRepository.GetByTitleAndYear(mtitle, myear);
                        if (f == null)
                        {
                            throw new UnknownFilmException(mtitle, myear);
                        }
                        return new Core.Entities.Medium(f.Id, mediumType);
                    case "FilmPeopleController":
                        var title2 = ((BaseFilmPersonDto)arg).Title;
                        var year2 = ((BaseFilmPersonDto)arg).Year;
                        var lastName2 = ((BaseFilmPersonDto)arg).LastName;
                        var birthdate2 = ((BaseFilmPersonDto)arg).Birthdate;
                        var role = ((BaseFilmPersonDto)arg).Role;
                        var f1 = _filmRepository.GetByTitleAndYear(title2, year2);
                        var p = _personRepository.GetByLastNameAndBirthdate(lastName2, birthdate2);
                        if (f1 == null)
                        {
                            throw new UnknownFilmException(title2, year2);
                        }
                        if (p == null)
                        {
                            throw new UnknownPersonException(lastName2, birthdate2);
                        }
                        return new Core.Entities.FilmPerson(f1.Id, p.Id, role);
                    default:
                        throw new Exception($"Unknown Controller {controllerName}");
                }
            }
        }
    }
}
