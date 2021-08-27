using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace WpfApp1_HW5
{
    public class Folder: INotifyPropertyChanged
    {
        private string _name;
        private ObservableCollection<Folder> _items = new ObservableCollection<Folder>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public ObservableCollection<Folder> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {    
            get => _name;            
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}
