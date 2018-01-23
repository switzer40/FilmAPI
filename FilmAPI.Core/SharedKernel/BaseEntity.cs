using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.SharedKernel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public abstract void Copy(BaseEntity e);
    }
}
