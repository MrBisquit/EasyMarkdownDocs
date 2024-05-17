namespace EasyMarkdownDocs.Core
{
    public static class PageTypes
    {
        public class Directory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string RelativeLocation { get; set; }

            public List<Page> Pages { get; set; } = new List<Page>();
        }
        public class Page
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string RelativeLocation { get; set; }

            public List<PageElement> Elements { get; set; } = new List<PageElement>();
        }

        /// <summary>
        /// Base of any element
        /// </summary>
        public class PageElement
        {
            public string Id { get; set; } // Guid.NewGuid().ToString()
            public string Name { get; set; } // Nice name
            public extern string GenerateContent();
        }
    }
}
