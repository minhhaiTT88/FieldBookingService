using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace FieldBookingService
{
    public interface IWebsocketHandler
    {
        Task Handle(Guid id, WebSocket websocket);
    }
}