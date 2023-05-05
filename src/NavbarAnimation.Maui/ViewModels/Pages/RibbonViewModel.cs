using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using IdentityModel.OidcClient;

using NavbarAnimation.Maui.DataStores;
using NavbarAnimation.Maui.Models.Locals;
using NavbarAnimation.Maui.Models.Respones.Tickets;
using NavbarAnimation.Maui.ViewModels.Base;

namespace NavbarAnimation.Maui.ViewModels.Pages;

/// <summary>
/// 
/// </summary>
public partial class RibbonViewModel : BaseListViewModel<Ticket, TicketResponse, TicketDataStore>
{
    private readonly OidcClient _client;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="dataStore"></param>
    public RibbonViewModel(OidcClient client, TicketDataStore dataStore) : base(dataStore)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    [ObservableProperty]
    private string text;

    [RelayCommand]
    private async Task Login()
    {
        var result = await _client.LoginAsync();

        if (result.IsError)
        {
            Text = result.Error;
            return;
        }

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

        Text = sb.ToString();

        await SecureStorage.SetAsync("access_token", result.AccessToken);
    }
}
