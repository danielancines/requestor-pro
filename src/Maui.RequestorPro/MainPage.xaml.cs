using Maui.RequestorPro.ViewModels.Base;

namespace Maui.RequestorPro;

public partial class MainPage : ContentPage
{
    #region Constructor
    public MainPage(IMainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        this.BindingContext = mainPageViewModel;
    }

    #endregion
}
