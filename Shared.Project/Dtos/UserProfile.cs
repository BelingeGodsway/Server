using System.ComponentModel.DataAnnotations;
namespace Shared.Project.DTOs
{
    public class UserProfile
    {
        [Required]
        public string Mandant { get; set; } = string.Empty;
        [Required]
        public string Bezeichnung { get; set; } = string.Empty;
       }
}
