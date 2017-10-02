using FilmAPI.Core.SharedKernel;
using FilmAPI.Validators;
using FilmAPI.Validators.Film;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Tests.UnitTests
{
    public class ValidatorTestBase
    {
        protected FilmValidator filmValidator = new FilmValidator();        
        protected string LongString = "01234567890123456789012345678901234567890123456789x";
        protected string GoodDateString = DateTime.Today.ToString();
        protected string BadDateString = "2017-06-31";
        protected short EarlyYear = 1849;
        protected short LateYear = 2051;
        protected short GoodYear = 1957;
        protected string GoodTitle = "Star Wars";
        protected string GoodName = "Fisher";
        protected string BadMediimType = "Tape";
        protected string GoodMediumType = FilmConstants.MediumType_DVD;
        protected string GoodRole = FilmConstants.Role_Actor;
    }
}
