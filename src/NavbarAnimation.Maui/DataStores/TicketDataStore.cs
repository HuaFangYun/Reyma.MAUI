using System;

using NavbarAnimation.Maui.Models.Locals;
using NavbarAnimation.Maui.Models.Respones.Tickets;

namespace NavbarAnimation.Maui.DataStores;

/// <summary>
/// 
/// </summary>
public class TicketDataStore : ReymaBaseDataStore<Ticket, TicketResponse>
{
    public TicketDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected override string ApiUrl => "api/tickets/serviciosti/tickets";
}