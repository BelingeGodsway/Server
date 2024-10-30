using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class Ansprechpartner : BaseEntity
    {
        [Key]
        public int Pos { get; set; }
        public string Adresse { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; } = string.Empty;
        public string? Anrede { get; set; }
        public string? Titel { get; set; }
        public string? Vorname { get; set; }
        public string? Nachname { get; set; }
        public string? Telefon1 { get; set; }
        public string? Telefon2 { get; set; }
        public string? Mail { get; set; }
        public string? Mobil { get; set; }
        public string? Notiz { get; set; }
        public string? Funktion { get; set; }
        public string? Abteilung { get; set; }

        //public string? AdrSuchbegriff { get; set; }
    }
}
