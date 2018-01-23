using FilmAPI.Common.Constants;
using FilmAPI.Common.DTOs;
using FilmAPI.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Validation.Validators
{
    public class BaseValidator<T> : AbstractValidator<T> where T : IBaseDto
    {
        
        protected bool BeValidDate(string date)
        {
            DateTime parsedDate;
            return DateTime.TryParse(date, out parsedDate);
        }
        protected bool BeValidMediumType(string arg)
        {
            return (arg == FilmConstants.MediumType_BD) ||
                   (arg == FilmConstants.MediumType_DVD);
        }
        protected bool BeValidLocation(string arg)
        {
            return (arg == FilmConstants.Location_Left) ||
                    (arg == FilmConstants.Location_Left1) ||
                    (arg == FilmConstants.Location_Left2) ||
                    (arg == FilmConstants.Location_Left3) ||
                    (arg == FilmConstants.Location_Left4) ||
                    (arg == FilmConstants.Location_Right) ||
                    (arg == FilmConstants.Location_Right1) ||
                    (arg == FilmConstants.Location_Right2) ||
                    (arg == FilmConstants.Location_Right3) ||
                    (arg == FilmConstants.Location_Right4) ||
                    (arg == FilmConstants.Location_Top) ||
                    (arg == FilmConstants.Location_Middle) ||
                    (arg == FilmConstants.Location_Bottom) ||
                    (arg == FilmConstants.Location_BD1) ||
                    (arg == FilmConstants.Location_BD2) ||
                    (arg == FilmConstants.Location_BD3) ||
                    (arg == FilmConstants.Location_BD4) ||
                    (arg == FilmConstants.Location_Shelf1);
        }
        protected bool BeValidRole(string arg)
        {
            return (arg == FilmConstants.Role_Actor) ||
                    (arg == FilmConstants.Role_Composer) ||
                    (arg == FilmConstants.Role_Director) ||
                    (arg == FilmConstants.Role_Writer);
        }
    }
}
