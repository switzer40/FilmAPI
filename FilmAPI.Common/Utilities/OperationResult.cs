using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Utilities
{
    public class OperationResult<T> 
    {
        public OperationResult()
        {                        
        }
        public OperationResult(OperationStatus status, T value = default)
        {
            Status = status;
            Value = value;
            HasResult =
                (status == OperationStatus.OK);
        }
        public bool HasResult { get; set; }
        public OperationStatus Status { get; set; }
        public T Value { get; set; }
    }
}
