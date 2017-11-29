using FilmAPI.Common.Validators;
using FilmAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Tests.UnitTests
{
    public class ValidatorTestBase
    {        
        public ValidatorTestBase()
        {
            DateValidator = new DateValidator();
            FilmValidator = new FilmValidator();
            PersonValidator = new PersonValidator(DateValidator);
            MediumValidator = new MediumValidator();
            FilmPersonValidator = new FilmPersonValidator(DateValidator);
            

        }
        
        protected short GoodYear { get => (short)1957; }
        public DateValidator DateValidator { get; set; }
        public FilmValidator FilmValidator { get; set; }
        public FilmPersonValidator FilmPersonValidator { get; set; }
        public MediumValidator MediumValidator { get; set; }
        public PersonValidator PersonValidator { get; set; }
    }
}
