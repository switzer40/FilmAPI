using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class DateTimeUtility
    {
        public static bool ValidDate(string date)
        {
            DateTime parsedDate = new DateTime();
            return DateTime.TryParse(date, out parsedDate);
        }
    }
}
