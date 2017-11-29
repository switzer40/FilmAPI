using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Validators
{
    public class DateValidator : IDateValidator
    {
        public string DateAsString { get; set; }
        public DateValidator()
        {            
        }
        public bool Validate()
        {
            DateTime parsedDate;
            return DateTime.TryParse(DateAsString, out parsedDate);
        }
    }
}
