using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Utilities
{
    public class ListOperationResult
    {
        public ListOperationResult(OperationStatus status, List<IKeyedDto> value = null)
        {
            Status = status;
            ResultValue = value;
        }
        public OperationStatus Status { get; set; }
        public List<IKeyedDto> ResultValue { get; set; }
    }
}
