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
            DriveInfo[] dis1 = DriveInfo.GetDrives();
            FillFolders(Folders, dis1, dis1.Length, 2, true);
        }

        private void FillFolders(ObservableCollection<Folder> items, DriveInfo[] dis, int count, int depth, bool isRoot)
        {
            string name="";
            for (int i = 0; i < count; i++)
            {
                if (dis[i].IsReady) name = dis[i].Name;
                Folder folder = new Folder() { Name = name };
                items.Add(folder);
                
                try
                {
                    if (i <= count - 1 && depth > 1)
                    {                       
                        string[] subDis = Directory.GetDirectories(dis[i].Name);
                        string[] filesDis = Directory.GetFiles(dis[i].Name);
                        FillSubFolders(folder.Items, subDis, subDis.Length, filesDis, filesDis.Length, 2, false);
                    }
                }
                catch (UnauthorizedAccessException) { continue; }
            }
        }

        private void FillSubFolders(ObservableCollection<Folder> items, string[] dis, int countdis, string[] files, int countfiles, int depth, bool isRoot)
        {    
            for (int i = 0; i < countdis; i++)
            {
                var name=System.IO.Path.GetFileName(dis[i]);   
                Folder folder = new Folder() { Name = name };
                items.Add(folder);

                if (i <= countdis - 1 && depth > 1)
                {
                    try 
                    {
                        string[] subDis = Directory.GetDirectories(dis[i]);
                        string[] filesDis = Directory.GetFiles(dis[i]);
                        FillSubFolders(folder.Items, subDis, subDis.Length, filesDis, filesDis.Length, 2, false);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        //MessageBox.Show(e.Message);
                        continue;
                    }                    
                }                
            }
            
                for (int i = 0; i < countfiles; i++)
            {
                var name = System.IO.Path.GetFileName(files[i]);
                Folder folder = new Folder() { Name = name };
                items.Add(folder);                
            }
        }      
    }
}