using EasyMarkdownDocs.Core;
using Ookii.Dialogs.Wpf;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using static EasyMarkdownDocs.Core.PageTypes;

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

        // For this, what I need to do is turn the instance directories and entries into a different type of list with lists inside them, so it's easier to create the tree.
        // So, creating the list, searching for each element in it, finding the correct destination (Does directories, then files and then last elements)
        private void UpdateContentTree()
        {
            if (instance == null) return; // Don't do anything if it's not initialised

            ContentTree.Items.Clear(); // Clear the tree

            //List<Directory> dirs = new List<Directory>();
            Directory baseDir = new Directory() { Name = "Root", RelativeLocation = "\\" };

            // Add the directories
            for (int i = 0; i < instance.Directories.Count; i++)
            {
                PageTypes.Directory dir = instance.Directories[i];

                // Work out where to place things (Based on their parent director(y/ies))

                string[] parts = dir.Name.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                CreateRootDir(parts, baseDir);
            }

            // Loop over them again, this time to add the pages and their elements
            for (int i = 0; i < instance.Directories.Count; i++)
            {
                // Translate all of the pages and add them to the tree
                for(int j = 0; j < instance.Directories[i].Pages.Count; i++)
                {
                    File file = new File()
                    {
                        Name = instance.Directories[i].Pages[j].Name,
                        RelativeLocation = instance.Directories[i].Pages[j].RelativeLocation,
                        Elements = instance.Directories[i].Pages[j].Elements
                    };

                    Directory? result = AddToDirectoryIfCorrect(file.RelativeLocation.Split("\\"), baseDir, file);
                    baseDir.Subdirectories.Add(result);
                }
            }

            // Now go for the files at the root
            for (int i = 0; i < instance.RootDirectory.Pages.Count; i++)
            {
                baseDir.Files.Add(new File()
                {
                    Name = instance.RootDirectory.Pages[i].Name,
                    RelativeLocation = instance.RootDirectory.Pages[i].RelativeLocation,
                    Elements = instance.RootDirectory.Pages[i].Elements
                });
            }

            // Now we can actually get to the transforming it into a TreeView
            // Directories first and then the individual files
            for (int i = 0; i < baseDir.Subdirectories.Count; i++)
            {
                ContentTree.Items.Add(GenerateDirectoryItem(baseDir.Subdirectories[i]));
            }
            // Now for the individual files
            for (int i = 0; i < baseDir.Files.Count; i++)
            {
                ContentTree.Items.Add(GenerateFileItem(baseDir.Files[i]));
            }

            // Refresh content
            ContentTree.Items.Refresh();
            MessageBox.Show(ContentTree.Items.Count.ToString());
            MessageBox.Show(baseDir.Subdirectories.Count.ToString());
        }

        private TreeViewItem GenerateDirectoryItem(Directory dir)
        {
            TreeViewItem item = new TreeViewItem()
            {
                Header = dir.Name
            };

            for (int i = 0; i < dir.Subdirectories.Count; i++)
            {
                item.Items.Add(GenerateDirectoryItem(dir.Subdirectories[i]));
            }

            for (int i = 0; i < dir.Files.Count; i++)
            {
                item.Items.Add(GenerateFileItem(dir.Files[i]));
            }

            return item;
        }

        private TreeViewItem GenerateFileItem(File file)
        {
            TreeViewItem fileItem = new TreeViewItem()
            {
                Header = file.Name
            };

            // Loop through the elements
            for (int i = 0; i < file.Elements.Count; i++)
            {
                TreeViewItem element = new TreeViewItem()
                {
                    Header = file.Elements[i].Name
                };
            }

            return fileItem;
        }

        private Directory? AddToDirectoryIfCorrect(string[] parts, Directory baseDir, Directory dir)
        {
            if(baseDir.RelativeLocation.Split("\\") == parts)
            {
                baseDir.Subdirectories.Add(dir);

                return dir;
            }

            for (int i = 0; i < baseDir.Subdirectories.Count; i++)
            {
                Directory? result = AddToDirectoryIfCorrect(parts, baseDir.Subdirectories[i], dir);

                if(result != null) return result;
            }

            return null;
        }

        private Directory? AddToDirectoryIfCorrect(string[] parts, Directory baseDir, File file)
        {
            if (baseDir.RelativeLocation.Split("\\") == parts)
            {
                baseDir.Files.Add(file);

                return baseDir;
            }

            for (int i = 0; i < baseDir.Subdirectories.Count; i++)
            {
                Directory? result = AddToDirectoryIfCorrect(parts, baseDir.Subdirectories[i], file);

                if (result != null) return result;
            }

            return null;
        }

        private void CreateRootDir(string[] parts, Directory baseDir)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                string relativePath = i == 0 ? "" : JoinString(parts, 0, i - 1);
                Directory? result = AddToDirectoryIfCorrect(relativePath.Split("\\"), baseDir, new Directory() { Name = parts[i], RelativeLocation = relativePath });
            }
        }

        private string JoinString(string[] parts, int from, int to, string separator = "\\")
        {
            string newString = "";
            for (int i = from; i < to; i++)
            {
                newString += parts[i];
                if(i - 1 != to) newString += separator;
            }
            return newString;
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

                instance = new ProjectInfo();
                instance.Name = ofd.FileName;
                instance.Directories.Add(new PageTypes.Directory() { Name = "Test", RelativeLocation = "test" });
                instance.Directories.Add(new PageTypes.Directory() { Name = "Docs", RelativeLocation = "test\\docs" });
                instance.RootDirectory.Pages.Add(new PageTypes.Page() { Name = "README.md", RelativeLocation = "\\README.md", Elements = new List<PageElement> { new PageElement() { Name = "Test", Id = "FFFFFF" } } });

                UpdateContentTree();
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
                    "docs/resources/(empty) - For Images/Files\n" +
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