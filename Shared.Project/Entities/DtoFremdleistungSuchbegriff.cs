﻿
using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class DtoFremdleistungSuchbegriff : BaseEntity
    {
        [Key]
        public string Nummer { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; } = string.Empty;
        public string? Einheit { get; set; } = string.Empty;
        public string? Baum1 { get; set; } = string.Empty;
        public string? Baum2 { get; set; } = string.Empty;
    }
}