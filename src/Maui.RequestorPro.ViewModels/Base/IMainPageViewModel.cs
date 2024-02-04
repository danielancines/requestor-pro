using Utils.Commands;

namespace Maui.RequestorPro.ViewModels.Base;

public interface IMainPageViewModel
{
    ObservableCommand SendCommand { get; }
    public string Url { get; set; }
    public string Response { get; set; }
    public string Body { get; set; }
}
