using FilmAPI.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Core.Entities
{
    public class Medium : BaseEntity
    {        
        public Medium()
        {
        }
        public Medium(int filmId, string mediumType, string location = "")
        {
            FilmId = filmId;
            MediumType = mediumType;
            Location = location;
        }


        [Required]
        public int FilmId { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
        
        public override void Copy(BaseEntity e)
        {
            if (e.GetType() == typeof(Medium))
            {
                var t = (Medium)e;
                FilmId = t.FilmId;
                MediumType = t.MediumType;
                Location = t.Location;
            }
            else
            {
                throw new Exception($"Bad argument type: {e.GetType()}");
            }
        }
    }
}
