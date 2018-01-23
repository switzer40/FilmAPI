using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Utilities
{
    public class OperationStatus
    {
        public OperationStatus()
        {
        }
        public static OperationStatus OK = new OperationStatus(0, "OK", "");
        public static OperationStatus BadRequest = new OperationStatus(1, "BadRequest", "Malformed argument");
        public static OperationStatus NotFound = new OperationStatus(2, "NotFound", "Entity does not exist");
        public static OperationStatus ServerError = new OperationStatus(3, "ServerError", "The Server blew it");
        private OperationStatus(int value, string name, string reason)
        {
            Value = value;
            Name = name;
            ReasonForFailure = reason;
        }
        public int Value { get; set; }
        public string Name { get; set; }
        public string ReasonForFailure { get; set; }

    }
}
