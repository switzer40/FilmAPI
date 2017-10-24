using System;
using System.Collections.Generic;
using System.Text;

namespace FilmAPI.Core.SharedKernel
{
    public class FilmConstants
    {
        // Media Types

        public const string MediumType_DVD = "DVD";
        public const string MediumType_BD = "BD";

        // Roles
        public const string Role_Actor = "Actor";
        public const string Role_Composer = "Composer";
        public const string Role_Director = "Director";
        public const string Role_Writer = "Scriptwriter";

        // Locations
        public const string Location_Left = "Left Drawer";
        public const string Location_Right = "Right Drawer";
        public const string Location_Left1 = "Left DVD Rack Leve1";
        public const string Location_Left2 = "Left DVD Rack Leve2";
        public const string Location_Left3 = "Left DVD Rack Leve3";
        public const string Location_Left4 = "Left DVD Rack Leve4";
        public const string Location_Right1 = "Right DVD Rack Leve1";
        public const string Location_Right2 = "Right DVD Rack Leve2";
        public const string Location_Right3 = "Right DVD Rack Leve3";
        public const string Location_Right4 = "Right DVD Rack Leve4";
        public const string Location_Top = "Commode top drawer";
        public const string Location_Middle = "Commode middle drawer";
        public const string Location_Bottom = "Commode bottom drawer";
        public const string Location_BD1 = "BD Rack Level1";
        public const string Location_BD2 = "BD Rack Level2";
        public const string Location_BD3 = "BD Rack Level3";
        public const string Location_BD4 = "BD Rack Level4";
        public const string Location_Shelf1 = "Bedroom bookshelf1";

        // Routes
        public const string FilmUri = "api/films";
        public const string FilmPersonUri = "api/filmpeople";
        public const string MediumUri = "api/media";
        public const string PersonUri = "api/people";

        // Miscellaneous
        public const string BADKEY = "Bad Key";
        public const string FORTYTWO = "42";
        public const string ImprobableDateString = "1615-06-23";
        public const bool Force = false; // The original idea was a bad mistake; remove this some day.

        public static string ConnectionString { get; set; }
    }
}
