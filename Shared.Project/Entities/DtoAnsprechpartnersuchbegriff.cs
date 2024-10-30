using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class DtoAnsprechpartnersuchbegriff : BaseEntity
    {
        [Key]
        public int Pos { get; set; }
        public string? Suchbegriff { get; set; } = string.Empty;

        public string? AdrSuchbegriff { get; set; } = string.Empty;
    }
}
