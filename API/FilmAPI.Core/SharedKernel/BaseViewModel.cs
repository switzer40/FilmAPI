using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.SharedKernel
{
    public abstract class BaseViewModel
    {
        protected readonly IKeyService _keyService;
        public BaseViewModel(IKeyService service)
        {
            _keyService = service;
        }
        public abstract string SurrogateKey();

        public void Copy<ModelType>(ModelType u) where ModelType : BaseViewModel
        {
            throw new NotImplementedException();
        }
    }
}
