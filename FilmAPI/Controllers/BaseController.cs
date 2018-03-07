using System.Collections.Generic;
using System.Linq;
using FilmAPI.Common.Utilities;
using FilmAPI.Core.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace FilmAPI.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseEntity
    {
        protected IActionResult StandardReturn(OperationStatus status, T value = default)
        {
            if (status == OperationStatus.OK) 
            {
                return Ok(value);
                
            }
            else
            {
                return HandleError(status);
            }
        }
        protected IActionResult StandardCountReturn(OperationStatus status, int value = 0)
        {
            if (status== OperationStatus.OK)
            {
                return Ok(value);
            }
            else
            {
                return HandleError(status);
            }
        }
        
        private IActionResult HandleError(OperationStatus status)
        {
            IActionResult result = null;
            if (status == OperationStatus.BadRequest)
            {
                result = new BadRequestObjectResult(status.ReasonForFailure);
            }
            else
            {
                result = new NotFoundObjectResult(status.ReasonForFailure);
            }
            return result;
        }
    }
}
