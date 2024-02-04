using Maui.RequestorPro.ViewModels.Base;
using Utils.Commands;

namespace Maui.RequestorPro.ViewModels;

public class MainPageViewModel : Observable, IMainPageViewModel
{
    #region Fields

    private readonly IHttpClientFactory _httpClientFactory;

    #endregion

    #region Constructor

    public MainPageViewModel(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
        this.InitializeCommands();
    }

    #endregion

    #region Properties

    public ObservableCommand SendCommand { get; private set; }
    public string Body { get; set; }

    private string _url;
    public string Url
    {
        get { return this._url; }
        set
        {
            if (this._url == value)
                return;

            this._url = value;
            this.OnPropertyChanged();
        }
    }

    private string _response;
    public string Response
    {
        get { return this._response; }
        set
        {
            if (this._response == value)
                return;

            this._response = value;
            this.OnPropertyChanged();
        }
    }

    #endregion

    #region Private Methods

    private void InitializeCommands()
    {
        this.SendCommand = new ObservableCommand(this.OnSendRequest);
    }

    private async void OnSendRequest(object parameter)
    {
        this.Response = string.Empty;

        var httpClient = this._httpClientFactory.CreateClient();
        try
        {
            var payload = new StringContent(this.Body, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync(this.Url, payload);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            this.Response = responseContent;
        }
        catch (Exception ex)
        {
        }
    }

    #endregion
}
