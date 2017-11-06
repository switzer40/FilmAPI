using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.Exceptions
{
    public class UnknownPersonException : Exception
    {
        public UnknownPersonException(string lastName, string birthdate) : base($"Unknown person LastName: {lastName} Birthdate: {birthdate}")
        {
            LastName = lastName;
            Birthdate = birthdate;
        }
        public string LastName { get; set; }
        public string Birthdate { get; set; }

    }
}
