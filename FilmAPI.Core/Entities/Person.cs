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
                return DateTime.Parse(BirthdateString);
            }
        }
        public string FullName { get {
                return $"{FirstMidName} {LastName}";
            }
        }
    }
}


