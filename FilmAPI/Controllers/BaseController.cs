using FilmAPI.Core.SharedKernel;
using FilmAPI.Interfaces.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FilmAPI.Interfaces.Services;

namespace FilmAPI.Controllers
{
    public abstract class BaseController<T> : Controller,  IController<T> where T : BaseEntity
    {
        protected IService<T> _service;
        public BaseController(IService<T> service)
        {
            _service = service;
        }
        [HttpDelete("{key}")]
        public abstract Task<IActionResult> DeleteAsync(string key);

        [HttpGet]
        public abstract Task<IActionResult> GetAsync();


        [HttpGet("{key}")]
        public abstract Task<IActionResult> GetByKeyAsync(string key);



        [HttpPost]
        public abstract Task<IActionResult> PostAsync([FromBody]IBaseDto<T> b);

        [HttpPut]
        public abstract Task<IActionResult> PutAsync([FromBody]IBaseDto<T> b);
        
    }
}
