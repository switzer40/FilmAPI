using FilmAPI.Common.Utilities;
using FilmAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Services
{
    public class ErrorService : IErrorService
    {
        public ErrorService(OperationStatus status)
        {
            _errorStatus = status;
        }
        private OperationStatus _errorStatus;
        public OperationStatus ErrorStatus { get => _errorStatus; set => _errorStatus = value; }
    }
}
