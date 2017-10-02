using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs;
using FluentValidation;
using System;
using System.Linq;

namespace FilmAPI.Validators
{
    public class BaseModelValidator<ModelType> :AbstractValidator<ModelType> //where ModelType : BaseDto
    { 
        protected bool BeAValidDate(string arg)
        {
            if (arg == FilmConstants.ImprobableDateString)
            {
                return false;
            }
            return DateTime.TryParse(arg, out var result);
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
