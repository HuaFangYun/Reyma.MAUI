using NavbarAnimation.Maui.Models.Respones.Base;

namespace NavbarAnimation.Maui.Models.Respones.Tickets;

public class TicketResponse : BaseResponse
{
    public int Numero { get; set; }

    public string Telefono { get; set; }
}