using System.ComponentModel.DataAnnotations;

namespace Shared.Project.DTOs
{
    public class AccountBase
    {
       
        
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Bezeichnung { get; set; }

       // [DataType(DataType.Password)]
        [Required]
        public string? Passwort { get; set; }

        [Required]
        [MinLength(5)]
       
        public string? WebApi { get; set; }

        public int Mandant { get; set; } = 0;
    }

}
