using FilmAPI.Common.Utilities;
using FilmAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult StandardReturn(OperationResult res)
        {
            if (res.HasValue)
            {
                return Ok(res.ResultValue);
                
            }
            else
            {
                return HandleError(res.Status);
            }
        }
        protected IActionResult CountReturn(OperationResult res)
        {
            if (res.HasValue)
            {
                return Ok(res.ResultValue.Count);
            }
            else
            {
                return HandleError(res.Status);
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
