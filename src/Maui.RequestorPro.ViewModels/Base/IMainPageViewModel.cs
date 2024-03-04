using Utils.Commands;

namespace Maui.RequestorPro.ViewModels.Base;

public interface IMainPageViewModel
{
    List<HttpMethod> HttpMethods { get; }
    ObservableCommand SendCommand { get; }
    ObservableCommand ViewLoadedCommand { get; }
    string Url { get; set; }
    string Response { get; set; }
    string Body { get; set; }
    HttpMethod SelectedMethod { get; set; }
}
