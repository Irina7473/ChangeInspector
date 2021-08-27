using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ClassLibrary;

namespace WpfApp1_HW5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Folder> _folders;

        public ObservableCollection<Folder> Folders
        {
            get => _folders;
            set
            {
                _folders = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Folders = new ObservableCollection<Folder>();
            DriveInfo[] dis = DriveInfo.GetDrives();
            FillFolders(Folders, dis.Length, 2, true, dis);
        }

        private void FillFolders(ObservableCollection<Folder> items, int count, int depth, bool isRoot, DriveInfo[] dis)
        {
            string name="";
            for (int i = 0; i < count; i++)
            {
                if (dis[i].IsReady) name = dis[i].Name;
                Folder folder = new Folder() { Name = name };
                items.Add(folder);
                if (i == count - 1 && depth > 1)
                    FillFolders(folder.Items, count, depth - 1, false, dis);
            }
        }

        

        /*
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvItem = (TreeViewItem)sender;
            //MessageBox.Show("Узел " + tvItem.Header.ToString() + " раскрыт");

            DriveInfo[] dis = DriveInfo.GetDrives();
            try
            {
                foreach (DriveInfo di in dis)
                {
                    if (di.IsReady) Disk1.Header=di.Name;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.GetType().Name);
            }
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvItem = (TreeViewItem)sender;
            MessageBox.Show("Выбран узел: " + tvItem.Header.ToString());
        }
        */
    }
}
