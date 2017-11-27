using FilmAPI.Common.Interfaces;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FilmAPI.Common.DTOs
{
    public class BaseMediumDto : IBaseDto<Medium>
    {
        public BaseMediumDto(string title,
                             short year,
                             string mediumType,
                             string location = "")
        {
            Title = title;
            Year = year;
            MediumType = mediumType;
            Location = location;
        }
        
        public string Title { get; set; }
        
        public short Year { get; set; }
        
        public string MediumType { get; set; }
        public string Location { get; set; }
    }
}
