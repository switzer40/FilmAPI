using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    public abstract class NewBaseController<T> : Controller where T : BaseEntity
    {
        protected IService<T> _service;
        protected IErrorService _errorService;
        protected IKeyedDto _result;
        protected Dictionary<string, IKeyedDto> _getResults;
        public NewBaseController(IErrorService eservice)
        {
            _errorService = eservice;
        }
        
        public IKeyedDto GetByKeyResult(string key)
        {
            IKeyedDto result = null;
            if (_getResults.ContainsKey(key))
            {
                result = _getResults[key];
            }
            return result;
        }

        public IActionResult HandleError(OperationStatus status)
        {
            IActionResult result = null;
            _errorService.ErrorStatus = status;
            if (status == OperationStatus.BadRequest)
            {
                result = new BadRequestObjectResult(status.ReasonForFailure);
            }
            else
            {
                if (status == OperationStatus.NotFound)
                {
                    result = new NotFoundObjectResult(status.ReasonForFailure);
                }
            }
            return result;
        }
    }
}
