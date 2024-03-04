using Maui.RequestorPro.ViewModels.Base;
using System.Net.Http.Headers;
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
        this.InitializeHttpMethods();
    }

    #endregion

    #region Properties

    public List<HttpMethod> HttpMethods { get; private set; }
    public ObservableCommand SendCommand { get; private set; }
    public ObservableCommand ViewLoadedCommand { get; private set; }
    public string Body { get; set; }

    private HttpMethod _selectedMethod;
    public HttpMethod SelectedMethod
    {
        get { return this._selectedMethod; }
        set
        {
            if (this._selectedMethod == value)
                return;

            this._selectedMethod = value;

            if (this._selectedMethod != null)
                Preferences.Default.Set(nameof(this._selectedMethod), this._selectedMethod.Method);

            this.OnPropertyChanged();
        }
    }

    private string _url;
    public string Url
    {
        get { return this._url; }
        set
        {
            if (this._url == value)
                return;

            this._url = value;
            Preferences.Set(nameof(_url), value);
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
        this.ViewLoadedCommand = new ObservableCommand(this.OnViewLoaded);
    }

    private void OnViewLoaded(object parameter)
    {
        var savedMethod = Preferences.Default.Get(nameof(this._selectedMethod), HttpMethod.Get.Method);
        switch (savedMethod)
        {
            case "POST":
                this.SelectedMethod = HttpMethod.Post;
                break;
            default:
                this.SelectedMethod = HttpMethod.Get;
                break;
        }

        this.Url = Preferences.Get(nameof(_url), string.Empty);
    }

    private void InitializeHttpMethods()
    {
        this.HttpMethods = new List<HttpMethod>()
        {
            HttpMethod.Get,
            HttpMethod.Post
        };
    }
    private void OnSendRequest(object parameter)
    {
        this.Response = string.Empty;

        if (this.SelectedMethod == null)
            return;

        switch (this.SelectedMethod.ToString())
        {
            case "GET":
                _ = this.Get();
                break;
            case "POST":
                _ = this.Post();
                break;
        }
    }

    private async Task Post()
    {
        var httpClient = this._httpClientFactory.CreateClient();
        try
        {
            var payload = new StringContent(this.Body, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync(this.Url, payload);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            this.Response = responseContent;
        }
        catch (Exception ex)
        {
            this.Response = ex.Message;
        }
    }

    private async Task Get()
    {
        var httpClient = this._httpClientFactory.CreateClient();
        try
        {
            var response = await httpClient.GetAsync(this.Url);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            this.Response = responseContent;
        }
        catch (Exception ex)
        {
            this.Response = ex.Message;
        }
    }

    #endregion
}
