using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Core.Entities
{
    public class FilmPerson : BaseEntity
    {
        private FilmPerson()
        {
            // neede by EF
        }
        public FilmPerson(int filmId, int personId, string role)
        {
            FilmId = filmId;
            PersonId = personId;
            Role = role;
        }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public string Role { get; set; }

        public void Copy(FilmPerson t)
        {
            FilmId = t.FilmId;
            PersonId = t.PersonId;
            Role = t.Role;
        }

        public override void Copy(BaseEntity e)
        {
            if (e.GetType() == typeof(FilmPerson))
            {
                var t = (FilmPerson)e;
                FilmId = t.FilmId;
                PersonId = t.PersonId;
                Role = t.Role;
            }
            else
            {
                throw new Exception($"Bad argument type: {e.GetType()}");
            }
        }
    }
}
