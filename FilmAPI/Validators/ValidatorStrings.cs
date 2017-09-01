using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.Validators
{
    public class ValidatorStrings
    {
        public const string NotNullString = "may not be null";
        public const string NotEmptyString = "may not be the empty string";
        public const string AtMost50CharString = "may have at most 50 chars";
        public const string NotBefore1850String = "may not be before 1850";
        public const string NotAfter2100String = "may not be after 2100";
        public const string MustBeValifdDateString =  "must represent a valid date";
        public const string KnownRoleString = "must be a known role";
        public const string KnownMediaTypeString = "must be a known medium type";
    }
}
