using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Project.Entities
{
  public  class FileManagerDirectoryContent
    {

        public string Name { get; set; }  // Name of the file/folder
        public bool IsFile { get; set; }  // True if it's a file, false if it's a folder
        public long Size { get; set; }  // Size of the file in bytes
        public DateTime DateModified { get; set; }  // Last modified date
        public bool HasChild { get; set; }  // True if the folder has children
        public string Path { get; set; }  // 
    }
}
