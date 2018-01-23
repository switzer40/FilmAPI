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
        public Person(string lastName,
                      string birthdate,
                      string firstMidName = "")
        {
            LastName = lastName;
            BirthdateString = birthdate;
            FirstMidName = firstMidName;
            FullName = $"{FirstMidName} {LastName}";
            Birthdate = DateTime.Parse(birthdate);
        }
        public string FirstMidName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BirthdateString { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }

        public override void Copy(BaseEntity e)
        {
            if (e.GetType() == typeof(Person))
            {
                var that = (Person)e;
                LastName = that.LastName;
                BirthdateString = that.BirthdateString;
                FirstMidName = that.FirstMidName;
                FullName = that.FullName;
                Birthdate = that.Birthdate;
            }
        }
    }
}
