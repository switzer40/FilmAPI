using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class RoleNameUtility
    {
        public static bool ValidRoleName(string role)
        {
            return (role == FilmConstants.Role_Actor) ||
                   (role == FilmConstants.Role_Composer) ||
                   (role == FilmConstants.Role_Director) ||
                   (role == FilmConstants.Role_Writer);

        }
    }
}
