using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class MediumTypeUtility
    {
        public static bool ValidMediumType(string type)
        {
            return ((type == FilmConstants.MediumType_BD) || (type == FilmConstants.MediumType_DVD));
        }
    }
}
