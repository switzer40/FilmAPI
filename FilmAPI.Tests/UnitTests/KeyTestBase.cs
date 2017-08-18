using FilmAPI.Interfaces;
using FilmAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Tests.UnitTests
{
    public class KeyTestBase
    {
        protected readonly IKeyService _keyService;
        public KeyTestBase()
        {
            _keyService = new KeyService();
        }
    }
}
