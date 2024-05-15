using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        // Functions
        public static void Save(string location, ProjectInfo instance)
        {
            string jsonString = JsonSerializer.Serialize(instance);

            File.WriteAllText(location, jsonString, Encoding.UTF8);
        }
        public static ProjectInfo? Load(string location)
        {
            if(!File.Exists(location)) return null;
            string jsonString = File.ReadAllText(location);

            try
            {
                return JsonSerializer.Deserialize<ProjectInfo>(jsonString);
            } catch
            {
                return null;
            }
        }
    }
}
