using System.Text;

using IdentityModel.OidcClient;

using NavbarAnimation.Maui.DataStores;

namespace NavbarAnimation.Maui.Views.Pages;

public partial class RibbonPage : ContentPage
{
    private readonly OidcClient _client;
    private readonly TicketDataStore _ticketDataStore;
    private string _currentAccessToken;

    public RibbonPage(OidcClient client, TicketDataStore ticketDataStore)
    {
        InitializeComponent();

        _client = client ?? throw new ArgumentNullException(nameof(client));
        _ticketDataStore = ticketDataStore ?? throw new ArgumentNullException(nameof(ticketDataStore));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var result = await _client.LoginAsync();

        if (result.IsError)
        {
            editor.Text = result.Error;
            return;
        }

        _currentAccessToken = result.AccessToken;

        var sb = new StringBuilder(128);

        sb.AppendLine("claims:");
        foreach (var claim in result.User.Claims)
        {
            sb.AppendLine($"{claim.Type}: {claim.Value}");
        }

        sb.AppendLine();
        sb.AppendLine("access token:");
        sb.AppendLine(result.AccessToken);

        if (!string.IsNullOrWhiteSpace(result.RefreshToken))
        {
            sb.AppendLine();
            sb.AppendLine("access token:");
            sb.AppendLine(result.AccessToken);
        }

        editor.Text = sb.ToString();

        await SecureStorage.SetAsync("access_token", result.AccessToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
        var accessToken = await SecureStorage.GetAsync("access_token");
        var ticket = await _ticketDataStore.GetEntity(new Guid("018714c2-9380-401f-a799-1f09c01db275"));
        var paged = await _ticketDataStore.GetList();

        _ = string.Empty;
    }
}
