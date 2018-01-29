using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.Utilities
{
    public interface ICopyable
    {
        void Copy(ICopyable arg);
    }
}
