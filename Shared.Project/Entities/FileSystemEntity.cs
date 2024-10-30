
namespace Shared.Project.Entities
{
    public class FileSystemEntity
    {
        public int Id { get; set; }
        public int? ParentId { get; set; } 
        public string Name { get; set; }
        public string Path { get; set; }
        public string? Description { get; set; } 
        public bool FileType { get; set; }
     

    }
}
    