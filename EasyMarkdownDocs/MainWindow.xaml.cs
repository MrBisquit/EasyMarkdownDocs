using EasyMarkdownDocs.Core;
using Ookii.Dialogs.Wpf;
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

        ProjectInfo instance;

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

        private void MMFOpen_Click(object sender, RoutedEventArgs e)
        {
            VistaOpenFileDialog ofd = new VistaOpenFileDialog();
            if((bool)ofd.ShowDialog())
            {
                instance = ProjectInfo.Load(ofd.FileName);
                
                if(instance == null)
                {
                    MessageBox.Show("Failed to load EasyMarkdownDocs instance", "EasyMarkdownDocs - Failed", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
                }
            }
        }

        private void MMFOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            if((bool)fbd.ShowDialog())
            {
                TaskDialog init = new TaskDialog()
                {
                    WindowTitle = "EasyMarkdownDocs",
                    Content = "Would you like to initialise documentation for this project? The structure will gain these (If it does not already have them):\n" +
                    "docs/README.md\n" +
                    "README.md\n\n" +
                    "It will also automatically gain an EasyMarkdownDocs.json file for containing information to be able to generate the markdown docs for you."
                };

                TaskDialogButton yes = new TaskDialogButton() { ButtonType = ButtonType.Yes };
                TaskDialogButton no = new TaskDialogButton() { ButtonType = ButtonType.No };

                init.Buttons.Add(yes);
                init.Buttons.Add(no);

                TaskDialogButton initResult = init.ShowDialog();
            }
        }
    }
}