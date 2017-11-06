using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;
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
    public class ValidateEntityExistsAttribute : TypeFilterAttribute
    {
        public ValidateEntityExistsAttribute() : base(typeof(ValidateEntityExistsFilterImpl))
        {
        }
        private class ValidateEntityExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IFilmRepository _filmRepository;
            private readonly IPersonRepository _personRepository;
            private readonly IMediumRepository _mediumRepository;
            private readonly IFilmPersonRepository _filmPersonRepository;
            private IKeyService _keyService;
            public ValidateEntityExistsFilterImpl(IFilmRepository frepo,
                                                  IPersonRepository prepo,
                                                  IMediumRepository mrepo,
                                                  IFilmPersonRepository fprepo)
            {
                _filmRepository = frepo;
                _personRepository = prepo;
                _mediumRepository = mrepo;
                _filmPersonRepository = fprepo;
                _keyService = new KeyService();
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("key"))
                {
                    var key = (string)context.ActionArguments["key"];
                    BaseEntity model = null;
                    try
                    {
                        model = ExtractEntity(context, key);
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                    {

                        throw;
                    }
                    if (model == null)
                    {
                        context.Result = new BadRequestObjectResult(key);
                        return;
                    }
                }
                await next();
            }

            private BaseEntity ExtractEntity(ActionExecutingContext context, string key)
            {
                var controllerName = context.Controller.GetType().Name;
                switch (controllerName)
                {
                    case "FilmsController":
                        return _filmPersonRepository.GetByKey(key);
                    case "PeopleController":
                        return _personRepository.GetByKey(key);
                    case "MediaController":
                        return _mediumRepository.GetByKey(key);
                    case "FilmPeopleController":
                        return _filmPersonRepository.GetByKey(key);
                    default:
                        throw new Exception($"Unknown controller: {controllerName}");
                }
            }
        }
    }
}
