using System.ComponentModel;

namespace WpfList
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        protected void OnPermit(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
