using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMarkdownDocs
{
    public class Directory
    {
        public string RelativeLocation { get; set; } = "\\";
        public string Name { get; set; } = "";

        public List<Directory> Subdirectories { get; set; } = new List<Directory>();
        public List<File> Files { get; set; } = new List<File>();
    }

    public class File
    {
        public string RelativeLocation { get; set; } = "\\";
        public string Name { get; set; } = "";

        public List<Core.PageTypes.PageElement> Elements { get; set; } = new List<Core.PageTypes.PageElement>();
    }
}
