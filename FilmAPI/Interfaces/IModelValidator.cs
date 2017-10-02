using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;
using FluentValidation;

namespace FilmAPI.Interfaces
{
    public interface IModelValidator<ModelType> :  IValidator<ModelType> where ModelType : BaseFilmDto
    {
    }
}
