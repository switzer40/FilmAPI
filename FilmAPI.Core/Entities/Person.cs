using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Core.Entities
{
    public class Person : BaseEntity
    {
        public Person()
        {
        }
        public Person(string lastName, string birthdate, string firstMidName = "")
        {
            LastName = lastName;
            BirthdateString = birthdate;
            FirstMidName = firstMidName;
        }
        public string FirstMidName { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string LastName { get; set; }
        [Required]

        public string BirthdateString { get; set; }
        public DateTime Birthdate
        {
            get
            {
                // This looks strange; we don´t want to throw an
                // exception, if the user enters an invalid date string.
                // Some other mechanism will have to prevent that from happening.
                DateTime result = DateTime.Parse(FilmConstants.ImprobableDateString);
                DateTime dummy = DateTime.Now;
                if (DateTime.TryParse(BirthdateString, out dummy))
                {
                    result = dummy;
                }
                return result;
            }        
        }
        public string FullName { get
            {
                return $"{FirstMidName} {LastName}";
            }
        }
    }
}


