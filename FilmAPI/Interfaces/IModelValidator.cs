using FilmAPI.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Interfaces
{
    public interface IModelValidator<ModelType> :  IValidator<ModelType> where ModelType : BaseViewModel
    {
    }
}
