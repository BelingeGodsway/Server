
using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class Lohnarten : BaseEntity
    {
        [Key]
        public string Lohnart { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; } = string.Empty;
        public string? Einheit { get; set; } = string.Empty;
        public double? Preiseinheit { get; set; } = 0;
        public double? EKPreis { get; set; } = 0;
        public double? Zuschlag1 { get; set; } = 0;
        public double? Zuschlag2 { get; set; } = 0;
        public double? VK1 { get; set; } = 0;
        public string? Notiz { get; set; } = string.Empty;
    }
}
