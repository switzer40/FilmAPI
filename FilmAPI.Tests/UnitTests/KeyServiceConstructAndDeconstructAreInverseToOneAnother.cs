using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FilmAPI.Tests.UnitTests
{
    public class KeyServiceConstructAndDeconstructAreInverseToOneAnother : KeyTestBase
    {
        [Fact]
        public void ForFilms()
    {
        var dogTitle = "Lassie";
        var dogYear = (short)1954;
        var key = _keyService.ConstructFilmKey(dogTitle, dogYear);
        var data = _keyService.DeconstructFilmKey(key);
        Assert.Equal(dogTitle, data.title);
        Assert.Equal(dogYear, data.year);
    }
       
    }
}
