using Utils.Commands;

namespace Maui.RequestorPro.ViewModels.Base;

public interface IMainPageViewModel
{
    Dictionary<string, HttpMethod> HttpMethods { get; }
    ObservableCommand SendCommand { get; }
    string Url { get; set; }
    string Response { get; set; }
    string Body { get; set; }
}
