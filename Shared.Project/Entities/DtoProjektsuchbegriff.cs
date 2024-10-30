
using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class DtoProjektsuchbegriff : BaseEntity, IEquatable<DtoProjektsuchbegriff>
    {
        [Key]
        public string Projektnr { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; } = string.Empty;

        public string? AdrSuchbegriff { get; set; } = string.Empty;

        public bool Equals(DtoProjektsuchbegriff? other)
        {
            if (other == null) return false;
            return Projektnr == other.Projektnr && Suchbegriff == other.Suchbegriff && other.AdrSuchbegriff == AdrSuchbegriff;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Projektnr, Suchbegriff, AdrSuchbegriff);
        }
    }
}
