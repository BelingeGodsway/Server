

using System.ComponentModel.DataAnnotations;

namespace Shared.Project.Entities
{
 public class UploadResult
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
        public string? Projektnr { get; set; }
    }
}
