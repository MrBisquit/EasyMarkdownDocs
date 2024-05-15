using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMarkdownDocs.Core.Elements
{
    public class TextBlock : PageTypes.PageElement
    {
        public TextBlock(string Id)
        {
            this.Id = Id;
        }

        public TextBlock(string Id, string Content)
        {
            this.Id = Id;
            this.Content = Content;
        }

        public string Content { get; set; }
    }
}
