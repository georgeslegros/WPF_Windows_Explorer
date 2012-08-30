using System;
using System.Collections.ObjectModel;
using System.IO;

namespace CustomExplorer
{
    public class ExplorerNode : ViewModelBase
    {
        public string Header { get; set; }
        public string Path { get; set; }
        public ExplorerType ExplorerType { get; set; }
        public ObservableCollection<ExplorerNode> Children { get; set; }
        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                Set(ref isExpanded, value, () => IsExpanded);
                if (value) Expand();
            }
        }

        private void Expand()
        {
            ExplorerNode dummyExplorerNode = new ExplorerNode { ExplorerType = ExplorerType.Dummy };
            if (Children.Count == 1 && Children[0].ExplorerType == ExplorerType.Dummy)
            {
                Children.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(Path))
                    {
                        Children.Add(new ExplorerNode
                                         {
                                             Header = s.Substring(s.LastIndexOf("\\") + 1),
                                             Path = s,
                                             ExplorerType = ExplorerType.Folder,
                                             Children = new ObservableCollection<ExplorerNode> { dummyExplorerNode }
                                         });
                    }
                    foreach (string s in Directory.GetFiles(Path))
                    {
                        Children.Add(new ExplorerNode
                                         {
                                             Header = s.Substring(s.LastIndexOf("\\") + 1),
                                             Path = s,
                                             ExplorerType = ExplorerType.File
                                         });
                    }
                }
                catch (Exception) { }
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { Set(ref isSelected, value, () => IsSelected); }
        }

    }
}