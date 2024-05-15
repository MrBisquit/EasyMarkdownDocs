using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMarkdownDocs.Core.Elements
{
    public class TableOfContents : PageTypes.PageElement
    {
        // An automatically updating table of contents for a specific page
        // A page version of this will also be available.

        public TableOfContents(string Id)
        {
            this.Id = Id;
        }
    }
}
