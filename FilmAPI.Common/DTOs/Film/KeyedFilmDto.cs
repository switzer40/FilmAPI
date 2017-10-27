using FilmAPI.Common.Interfaces;

namespace FilmAPI.Common.DTOs.Film
{
    public class KeyedFilmDto : BaseFilmDto, IKeyedDto
    {
        public KeyedFilmDto(string title, short year, short length = 0, string key = "") : base(title, year, length)
        {
            _key = key;
        }
        private string _key;
        public string Key { get => _key; set => _key = value; }
    }
}
