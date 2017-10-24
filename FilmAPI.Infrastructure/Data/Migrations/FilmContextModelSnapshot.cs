using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FilmAPI.Infrastructure.Data;

namespace FilmAPI.Infrastructure.Data.Migrations
{
    [DbContext(typeof(FilmContext))]
    partial class FilmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FilmAPI.Core.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Length");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<short>("Year");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("FilmAPI.Core.Entities.FilmPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FilmId");

                    b.Property<int>("PersonId");

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FilmPeople");
                });

            modelBuilder.Entity("FilmAPI.Core.Entities.Medium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FilmId");

                    b.Property<string>("Location");

                    b.Property<string>("MediumType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("FilmAPI.Core.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BirthdateString")
                        .IsRequired();

                    b.Property<string>("FirstMidName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("People");
                });
        }
    }
}
