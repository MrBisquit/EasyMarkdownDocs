using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMarkdownDocs.Core
{
    /// <summary>
    /// Project information (It'll add a little json file containing all of the information for re-generating things)
    /// </summary>
    public class ProjectInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public PageTypes.Directory RootDirectory;     // Any files existing in the relative path of `/`
        public List<PageTypes.Directory> Directories; // Any directories (Subdirectories are also contained within here to avoid any complications and make it easier to manage)
    }
}
