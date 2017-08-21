using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.ViewModels
{
    public abstract class BaseViewModel
    {
        public abstract string SurrogateKey { get; set; }
    }
}
