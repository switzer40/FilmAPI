using FilmAPI.Interfaces;
using FilmAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public abstract class BaseViewModel
    {
        protected readonly IKeyService _keyService;
        public BaseViewModel()
        {
            _keyService = new KeyService();
        }
        public abstract string SurrogateKey { get;  }
    }
}
