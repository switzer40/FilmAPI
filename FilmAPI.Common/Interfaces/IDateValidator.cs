using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Interfaces
{
    public interface IDateValidator
    {
        string DateAsString { get; set; }
        bool Validate();
    }
}
