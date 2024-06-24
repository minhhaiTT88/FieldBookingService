using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace FieldBookingService.Controllers
{

    [Route("api/websocket")]
    public class WebSocketController : Controller
    {
        public IWebsocketHandler WebsocketHandler { get; }

        public WebSocketController(IWebsocketHandler websocketHandler)
        {
            WebsocketHandler = websocketHandler;
        }

        [HttpGet]
        public async Task Get(string username)
        {
            if(username != "hello")
            {
                return;
            }

            var context = ControllerContext.HttpContext;
            //var user = this.HttpContext.GetCurrentUser();
            //if (user == null)
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    return;
            //}

            var isSocketRequest = context.WebSockets.IsWebSocketRequest;

            if (isSocketRequest)
            {
                WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();

                await WebsocketHandler.Handle(Guid.NewGuid(), websocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }

}
