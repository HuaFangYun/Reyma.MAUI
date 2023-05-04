using System;
using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;

namespace NavbarAnimation.Maui.Auth;

/// <summary>
/// 
/// </summary>
public class ReymaMauiAuthenticationBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await WebAuthenticator.Default.AuthenticateAsync(
                new Uri(options.StartUrl),
                new Uri(options.EndUrl));

            var url = new RequestUrl("reyma.ti.mobile://callback")
                .Create(new Parameters(result.Properties));

            return new BrowserResult
            {
                Response = url,
                ResultType = BrowserResultType.Success
            };
        }
        catch (TaskCanceledException)
        {
            return new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };
        }
    }
}

