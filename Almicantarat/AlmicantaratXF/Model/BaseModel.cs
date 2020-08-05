using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;

namespace AlmicantaratXF.ViewModels
{
    public class BaseModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            //OnPropertyChanged(() => e);//If the contents of the property changed, then the property itself changed as far as binding is concerned
            //On the downside any ListView will see the entire collection as changed and repaint the entire control.
        }
    }
}