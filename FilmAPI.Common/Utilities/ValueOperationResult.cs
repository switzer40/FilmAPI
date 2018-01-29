using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Utilities
{
    public class ValueOperationResult
    {
        public ValueOperationResult(int value,  OperationStatus status = null)
        {
            Status = status;
            ReturnValue = value;
        }
        public OperationStatus Status { get; set; }
        public int ReturnValue { get; set; }
    }
}
