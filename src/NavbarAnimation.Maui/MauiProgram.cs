using IdentityModel.OidcClient;

using NavbarAnimation.Maui.Auth;
using NavbarAnimation.Maui.DataStores;
using NavbarAnimation.Maui.Views.Pages;

using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;

namespace NavbarAnimation.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Comfortaa-Regular.ttf", "RegularFont");
                    fonts.AddFont("Comfortaa-Bold.ttf", "BoldFont");
                    fonts.AddFont("Comfortaa-Medium.ttf", "MediumFont");
                    fonts.AddFont("Comfortaa-SemiBold.ttf", "SemiBoldFont");
                })
                .UseSimpleToolkit()
                .UseSimpleShell();

#if ANDROID || IOS
            builder.DisplayContentBehindBars();
#endif

            builder.Services.AddSingleton<RibbonPage>();

            builder.Services.AddSingleton(new OidcClient(new()
            {
                Authority = "https://www.benol.com.mx:5000/auth/dev",

                ClientId = "rym.ti.mobile",
                ClientSecret = "UmV5bWEgVEktYjZmZTYxMjctODRlNC00YjJjLTkyNTctNDkzOWQxN2JhNzYwLTIwMjIwMTA2",
                Scope = string.Join(" ", OidcConstants.Scopes),
                RedirectUri = "rym.ti.mobile://callback",

                Browser = new ReymaMauiAuthenticationBrowser()
            }));

            builder.Services.AddHttpClient("ReymaMobileHttpClient", async (sp, httpClient) =>
            {
                var oidcClient = sp.GetRequiredService<OidcClient>();

                httpClient.BaseAddress = new Uri("https://www.benol.com.mx:5000/reyma/ti/dev/");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{await SecureStorage.GetAsync("access_token")}");
            });

            // /api/tickets/serviciosti/tickets

            // DataStores
            builder.Services.AddScoped(typeof(TicketDataStore));
            
            return builder.Build();
        }
    }
}

internal class OidcConstants
{
    public static List<string> Scopes => new List<string>
    {
        "openid", "profile", "offline_access", "organizational", "permissions.service.read", "rym.ti.gateway.write", "rym.ti.gateway.read", "rym.ti.tickets.write", "rym.ti.tickets.read", "rym.ti.reminders.read", "rym.ti.reminders.write"
    };
}