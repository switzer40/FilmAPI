using System;

namespace FilmAPI.Services
{
    public class BadKeyException : Exception
    {
        public BadKeyException()
        {
        }

        public BadKeyException(string message) : base(message)
        {
        }

        public BadKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}