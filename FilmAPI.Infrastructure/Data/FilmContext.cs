using FilmAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Infrastructure.Data
{
    public class FilmContext :DbContext
    {
        public FilmContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmPerson> FilmPeople { get; set; }
        public DbSet<Medium> Media { get; set; }
        public DbSet<Person> People { get; set; }
    }
}
