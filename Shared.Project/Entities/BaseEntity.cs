using System.ComponentModel.DataAnnotations;
namespace Shared.Project.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Mandant { get; set; } = 0;
    
    }
}
