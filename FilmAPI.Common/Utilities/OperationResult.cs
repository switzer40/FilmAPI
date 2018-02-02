using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Utilities
{
    public class OperationResult
    {
        public OperationResult()
        {
            Status = OperationStatus.OK;
            ResultValue = new List<IKeyedDto>();
            HasValue = false;
        }
        public OperationResult(OperationStatus status, List<IKeyedDto> result = null)
        {
            Status = status;
            ResultValue = result;
            HasValue = (result != null);
        }
        public bool HasValue { get; set; }
        public OperationStatus Status { get; set; }
        public List<IKeyedDto> ResultValue { get; set; }
    }
}
