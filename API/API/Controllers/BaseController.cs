using AutoMapper;
using FilmAPI.Core.Interfaces;
using FilmAPI.Core.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BaseController<EntityType, ModelType> : Controller
        where ModelType : BaseViewModel
        where EntityType : BaseEntity
    {
        protected readonly IRepository<EntityType> _repository;
        protected readonly IMapper _mapper;
        
        public BaseController(IRepository<EntityType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }

    }
}
