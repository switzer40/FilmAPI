using FilmAPI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BaseMediumDto : IBaseDto
    {
        public BaseMediumDto()
        {
        }
        public BaseMediumDto(string title,
                             short year,
                             string mediumType,
                             string location = "",
                             bool german = true)
        {
            Title = title;
            Year = year;
            MediumType = mediumType;
            Location = location;
            HasGermanSubtitles = german;
        }
        public string Title { get; set; }
        public short Year { get; set; }
        public string MediumType { get; set; }
        public string Location { get; set; }
        public bool HasGermanSubtitles { get; set; }

        public virtual void Copy(IBaseDto dto)
        {
            if (dto.GetType() == typeof(BaseMediumDto))
            {
                var that = (BaseMediumDto)dto;
                Title = that.Title;
                Year = that.Year;
                MediumType = that.MediumType;
                Location = that.Location;
                HasGermanSubtitles = that.HasGermanSubtitles;
            }
        }
        public virtual bool Equals(IBaseDto dto)
        {
            var result = false;
            if (dto.GetType() == typeof(BaseMediumDto))
            {
                var that = (BaseMediumDto)dto;
                result = Title.Equals(that.Title) &&
                         Year.Equals(that.Year) &&
                         MediumType.Equals(that.MediumType) &&
                         Location.Equals(that.Location);
            }
            return result;
        }
    }
}
