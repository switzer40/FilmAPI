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
        var key = _keyService.ConstructFilmSurrogateKey(dogTitle, dogYear);
        var data = _keyService.DeconstructFilmSurrogateKey(key);
        Assert.Equal(dogTitle, data.title);
        Assert.Equal(dogYear, data.year);
    }
       
    }
}
