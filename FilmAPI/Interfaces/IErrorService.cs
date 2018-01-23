using FilmAPI.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IErrorService
    {
        OperationStatus ErrorStatus { get; set; }
    }
}
