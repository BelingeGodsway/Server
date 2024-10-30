
//using System.ComponentModel.DataAnnotations;

//namespace Shared.Project.Entities
//{
//    public class DtoAdressensuchbegriff : BaseEntity
//    {
//        [Key]
//        public string Adresse { get; set; } = string.Empty;
//        public string? Suchbegriff { get; set; } = string.Empty;
//    }
//}
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
    public class DtoAdressensuchbegriff : BaseEntity, IEquatable<DtoAdressensuchbegriff>
    {
        [Key]
        public string Adresse { get; set; } = string.Empty;
        public string? Suchbegriff { get; set; } = string.Empty;

        // Implement IEquatable<DtoAdressensuchbegriff>
        public bool Equals(DtoAdressensuchbegriff? other)
        {
            if (other == null) return false;
            return Adresse == other.Adresse && Suchbegriff == other.Suchbegriff;
        }

        // Override Equals to use Equals(DtoAdressensuchbegriff) for comparison
        public override bool Equals(object? obj)
        {
            return Equals(obj as DtoAdressensuchbegriff);
        }

        // Override GetHashCode to be consistent with Equals
        public override int GetHashCode()
        {
            return HashCode.Combine(Adresse, Suchbegriff);
        }
    }
}

