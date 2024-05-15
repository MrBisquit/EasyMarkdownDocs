using System.Windows;

namespace EasyMarkdownDocs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResizeContent()
        {
            // Fetch the sizes
            double width = this.ActualWidth;
            double height = this.ActualHeight;

            // Resize content
            ContentTree.Width = width / 4; // Size it to 1/4 of the size of the window
            MainContent.Width = width / 4 * 3; // Size it to 3/4 (The rest) of the size of the window
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeContent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeContent();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ResizeContent();
        }
    }
}