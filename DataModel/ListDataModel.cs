using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ListDataModel : INotifyPropertyChanged
    {
        private ObservableCollection<ListModel> _listdatas;

        public ObservableCollection<ListModel> ListDatas
        {
            get { return _listdatas; }
            set
            {
                if (_listdatas != value)
                {
                    _listdatas = value;
                    OnPropertyChanged("ListDatas");
                }
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
