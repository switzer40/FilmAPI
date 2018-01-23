using FilmAPI.Common.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Validation.Interfaces
{
    public interface IValidator<T> where T : IBaseDto
    {
        ValidationResult Validate(T t);
    }
}
