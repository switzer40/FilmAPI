using FilmAPI.Core.SharedKernel;
using FilmAPI.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class BaseModelValidator<ModelType> :AbstractValidator<ModelType> where ModelType : BaseViewModel
    {
        protected bool BeAValidDate(string arg)
        {
            return DateTime.TryParse(arg, out var redult);
        }
        private string[] _mediaTypes =
        {
            FilmConstants.MediumType_BD,
            FilmConstants.MediumType_DVD
        };

        protected bool BeAValidMediumType(string arg)
        {
            return _mediaTypes.Contains(arg);
        }
        private string[] _roleNames =
        {
            FilmConstants.Role_Actor,
            FilmConstants.Role_Composer,
            FilmConstants.Role_Director,
            FilmConstants.Role_Writer
        };        
        protected bool BeAValidRole(string arg)
        {
            return _roleNames.Contains(arg);
        }
    }
}
