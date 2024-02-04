using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.RequestorPro.ViewModels.Base;
public class Observable : INotifyPropertyChanged
{
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Protected Methods

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
