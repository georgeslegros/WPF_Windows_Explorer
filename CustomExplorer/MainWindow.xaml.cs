using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace CustomExplorer
{
    public partial class MainWindow 
    {
        public string SelectedImagePath { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private readonly ExplorerNode dummyExplorerNode = new ExplorerNode { ExplorerType = ExplorerType.Dummy };
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var nodes = Directory.GetLogicalDrives()
                .Select(s =>
                        new ExplorerNode
                            {
                                Header = s,
                                Path = s,
                                ExplorerType = ExplorerType.Drive,
                                Children = new ObservableCollection <ExplorerNode> {dummyExplorerNode}
                            }).ToList();

            foldersItem.ItemsSource = nodes;
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
