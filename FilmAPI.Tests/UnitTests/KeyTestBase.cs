using FilmAPI.Common.Interfaces;
using FilmAPI.Common.Services;

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
