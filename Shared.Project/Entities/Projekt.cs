using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class Projekt : BaseEntity
    {
      
        [Key]
        public string Projektnr { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; }

        public string? Projektbezeichnung1 { get; set; }
        public string? Projektbezeichnung2 { get; set; }
        public string? Projektname1 { get; set; }
        public string? Projektname2 { get; set; }
        public string? Projektstrasse { get; set; }
        public string? Projektintraland { get; set; }
        public string? Projektplz { get; set; }
        public string? Projektort { get; set; }
        public string? Niederlassung { get; set; }
        public string? Verantwortlicher { get; set; }
        public string? Vertrieb { get; set; }
        public string? Adresse { get; set; }
        public string? Ansprechpartner1 { get; set; }
        public string? Ansprechpartner2 { get; set; }

        public string? Telefon { get; set; }
        public string? Mail { get; set; }
        public string? Mobil { get; set; }
        public string? Projektgruppe1 { get; set; }

        public int? Status { get; set; }
        public DateTime? Beginn { get; set; }
        public DateTime? Ende { get; set; }

        public string? Notiz { get; set; }


        //public string? AdrSuchbegriff { get; set; }

      


    }
}
