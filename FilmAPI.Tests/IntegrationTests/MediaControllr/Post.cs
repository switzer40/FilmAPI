using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.SharedKernel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaControllr
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Post()
        {
            _client = base.GetClient();
            _route = FilmConstants.MediumUri;            
        }        
        [Fact]
        public async Task ReturnsOkGivenValidMedimDataAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var length = (short)109;
            var type = FilmConstants.MediumType_BD;
            var location = FilmConstants.Location_Left;
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var mediumToPost = new BaseMediumDto(title, year, type, location, length);
            var jsonString = JsonConvert.SerializeObject(mediumToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToPost), Encoding.UTF8, "application/json");
            var response = _client.PostAsync(_route, jsonContent).Result;
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);

            Assert.Equal(title, model.Title);
            Assert.Equal(year, model.Year);
            Assert.Equal(type, model.MediumType);
        }
        [Fact]
        public async Task ReturnsOkGvienValidDataAsync()
        {            
            var title = "Pretty Woman";
            var year = (short)1990;
            var length = (short)119;
            var type = FilmConstants.MediumType_BD;
            var location = FilmConstants.Location_BD3;                                    
            var response = await PostMediumAsync(title, year, type, location, length, _route);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task ReturnsBadRequestGivenTooEarlyYearAsync()
        {            
            var title = "Robzilla";
            var year = (short)1849;
            var length = (short)156;
            var type = FilmConstants.MediumType_BD;
            var location = FilmConstants.Location_BD3;                                    
            var response = await PostMediumAsync(title, year, type, location, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenTooLateYearAsync()
        {            
            var title = "Robzilla";
            var year = (short)2051;
            var length = (short)156;
            var type = FilmConstants.MediumType_BD;
            var location = FilmConstants.Location_BD3;                                    
            var response = await PostMediumAsync(title, year, type, location, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenEmptyTitleAsync()
        {            
            var title = "";
            var year = (short)2051;
            var length = (short)156;
            var type = FilmConstants.MediumType_BD;
            var location = FilmConstants.Location_BD3;                                    
            var response = await PostMediumAsync(title, year, type, location, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}