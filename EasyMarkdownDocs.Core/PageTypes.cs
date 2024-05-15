namespace EasyMarkdownDocs.Core
{
    public static class PageTypes
    {
        public class Page
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string RelativeLocation { get; set; }

            public List<PageElement> Elements { get; set; }
        }

        /// <summary>
        /// Base of any element
        /// </summary>
        public class PageElement
        {
            public string Id { get; set; } // Guid.NewGuid().ToString()
        }
    }
}
