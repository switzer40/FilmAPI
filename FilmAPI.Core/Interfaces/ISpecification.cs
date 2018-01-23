using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FilmAPI.Core.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}
