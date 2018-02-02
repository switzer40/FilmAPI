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
        public Medium(int filmId, string mediumType, string location = "", bool hasGermanSubtitles = true)
        {          
            FilmId = filmId;
            MediumType = mediumType;
            Location = location;
            HasGermanSubtitles = hasGermanSubtitles;
        }
        [Required]
        public int FilmId { get; set; }
        [Required]
        public string MediumType { get; set; }
        public string Location { get; set; }
        public bool HasGermanSubtitles { get; set; }
        public virtual Film Film { get; set; }

        public override void Copy(BaseEntity e)
        {
            if (e.GetType() == typeof(Medium))
            {
                var that = (Medium)e;
                FilmId = that.FilmId;
                MediumType = that.MediumType;
                Location = that.Location;
                HasGermanSubtitles = that.HasGermanSubtitles;
            }
        }
    }
}
