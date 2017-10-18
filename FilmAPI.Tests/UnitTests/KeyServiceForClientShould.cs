using FilmAPI.Interfaces;
using FilmAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class KeyServiceForClientShould
    {
        private IKeyService _apiService = new KeyService();
        private FilmAPI.ForClient.Interfaces.IKeyService _clientService = new FilmAPI.ForClient.Services.KeyService();
























            8
public IKeyService ApiService { get => _apiService; set => _apiService = value; }

        [Fact]
        public void AgreeWithAPIKeyServiceAboutConstructFilmKey()
    }
}
