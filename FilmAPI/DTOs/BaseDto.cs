using FilmAPI.Interfaces;
using FilmAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmAPI.DTOs
{
    public class BaseDto
    {
        [Required]
        public string SurrogateKey { get; set; }
    }
}
