﻿
using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class Leistungen : BaseEntity
    {
        [Key]
        public string Leistung { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; } = string.Empty;
        public string? Artikeltext { get; set; } = string.Empty;
        public string? Einheit { get; set; } = string.Empty;
        public string? Baum1 { get; set; } = string.Empty;
        public string? Baum2 { get; set; } = string.Empty;
        public double? Kalkekpreis { get; set; } = 0;
        public double? Zuschlag1 { get; set; } = 0;
        public double? Zuschlag2 { get; set; } = 0;
        public double? VK1 { get; set; } = 0;
        public string? Notiz { get; set; } = string.Empty;
    }
}
