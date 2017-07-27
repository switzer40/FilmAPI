using FilmAPI.Core.SharedKernel;
using System;
using System.Linq.Expressions;

namespace FilmAPI.Core.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}