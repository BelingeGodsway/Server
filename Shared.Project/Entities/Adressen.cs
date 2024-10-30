
using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class Adressen : BaseEntity
    {
        [Key]
        public string Adresse { get; set; } = string.Empty;
        public string?Suchbegriff { get; set; } = string.Empty;
        public string? Anrede { get; set; } = string.Empty;
        public string? Titel { get; set; } = string.Empty;
        public string? Vorname { get; set; } = string.Empty;
        public string? Nachname { get; set; } = string.Empty;
        public string?  Firma { get; set; } = string.Empty;
        public String? Namenszusatz { get; set; } = string.Empty;
        public string? Strasse { get; set; } = string.Empty;
        public string? PLZ { get; set; } = string.Empty;
        public string? Ort { get; set; } = string.Empty;
        public string? Intraland { get; set; } = string.Empty;
        public string? Telefon1 { get; set; } = string.Empty;
        public string? Telefon2 { get; set; } = string.Empty;
        public string? Mail { get; set; } = string.Empty;
        public string? Mobil { get; set; } = string.Empty;
        public string? Internet { get; set; } = string.Empty;
        public string? Prioritaet { get; set; } = string.Empty;
        public string? Notiz { get; set; } = string.Empty;
      }
}
