using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMarkdownDocs.Core.Elements
{
    /// <summary>
    /// A title
    /// </summary>
    public class Title : PageTypes.PageElement
    {
        public Title(string Id)
        {
            this.Id = Id;
        }

        public Title(string Id, TitleLevel level, string titleContent, string? titleDescription)
        {
            this.Id = Id;
            Level = level;
            TitleContent = titleContent;
            TitleDescription = titleDescription;
        }

        public TitleLevel Level { get; set; } = TitleLevel.Top;
        public string TitleContent { get; set; }
        public string? TitleDescription { get; set; } // A little bit of text underneath, like a short description

        public enum TitleLevel
        {
            Top =       1, // #
            TopMiddle = 2, // ##
            TopBottom = 3, // ###
            Bottom =    4  // ####
        }
    }
}
