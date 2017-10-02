using FluentValidation;
using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;

namespace FilmAPI.Validators.Film
{
    public class FilmValidator : BaseModelValidator<BaseFilmDto>
    {
        public FilmValidator()
        {
            RuleFor(bfd => bfd.Year).ExclusiveBetween((short)1850, (short)2050);
            RuleFor(bfd => bfd.Title).NotNull().NotEmpty().MaximumLength(50);
        }
    }    
}
