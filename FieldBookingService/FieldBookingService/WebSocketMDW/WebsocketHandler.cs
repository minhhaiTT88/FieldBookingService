using CommonLib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FieldBookingService
{
    public class WebsocketHandler : IWebsocketHandler
    {
        public static void StartPush()
        {
            var _PushData = new Thread(PushNotification) { IsBackground = true };
            _PushData.Start();
        }


        public static List<SocketConnection> websocketConnections = new List<SocketConnection>();

        public WebsocketHandler()
        {
            SetupCleanUpTask();
        }

        public async Task Handle(Guid id, WebSocket webSocket)
        {
            lock (websocketConnections)
            {
                websocketConnections.Add(new SocketConnection
                {
                    Id = id,
                    WebSocket = webSocket
                });
            }

            //await SendMessageToSockets($"User with id <b>{id}</b> has joined the chat");
            while (webSocket.State == WebSocketState.Open)
            {
                var message = await ReceiveMessage(id, webSocket);
                if (message != null)
                    await SendMessageToSockets(message);
            }
        }

        private async Task<string> ReceiveMessage(Guid id, WebSocket webSocket)
        {
            var arraySegment = new ArraySegment<byte>(new byte[4096]);
            var receivedMessage = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
            if (receivedMessage.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.Default.GetString(arraySegment).TrimEnd('\0');
                if (!string.IsNullOrWhiteSpace(message))
                    return $"<b>{id}</b>: {message}";
            }
            return null;
        }

        private async Task SendMessageToSockets(string message)
        {
            IEnumerable<SocketConnection> toSentTo;

            lock (websocketConnections)
            {
                toSentTo = websocketConnections.ToList();
            }

            var tasks = toSentTo.Select(async websocketConnection =>
            {
                if (websocketConnection.WebSocket.State == WebSocketState.Open)
                {
                    var bytes = Encoding.Default.GetBytes(message);
                    var arraySegment = new ArraySegment<byte>(bytes);
                    await websocketConnection.WebSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            });
            await Task.WhenAll(tasks);
        }

        private void SetupCleanUpTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    IEnumerable<SocketConnection> openSockets;
                    IEnumerable<SocketConnection> closedSockets;

                    lock (websocketConnections)
                    {
                        openSockets = websocketConnections.Where(x => x.WebSocket.State == WebSocketState.Open || x.WebSocket.State == WebSocketState.Connecting);
                        closedSockets = websocketConnections.Where(x => x.WebSocket.State != WebSocketState.Open && x.WebSocket.State != WebSocketState.Connecting);

                        websocketConnections = openSockets.ToList();
                    }

                    //foreach (var closedWebsocketConnection in closedSockets)
                    //{
                    //    await SendMessageToSockets($"User with id <b>{closedWebsocketConnection.Id}</b> has left the chat");
                    //}

                    await Task.Delay(5000);
                }

            });
        }

        private static void PushNotification()
        {
            while (true)
            {
                try
                {
                    //int _UrlChange = 1;
                    //string _msg = MemoryData.Dequeue_Push();
                    //if (_msg != null && _msg != "")
                    //{
                    //    var objJsonMarket = new JsonResultObj { Code = _UrlChange, Title = "UrlChange", Msg = _msg };
                    //    string _s = Newtonsoft.Json.JsonConvert.SerializeObject(objJsonMarket);
                    //    BroardCastMsg(_s);
                    //}

                    string _msg = "connected socket " + DateTime.Now.ToString();
                    BroardCastMsg(_msg);
                    //Task.Delay(3000);
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Logger.log.Error(ex.ToString());
                }
            }
        }

        public static void BroardCastMsg(string msg)
        {
            try
            {
                IEnumerable<SocketConnection> toSentTo;

                lock (websocketConnections)
                {
                    toSentTo = websocketConnections.ToList();
                }

                foreach (var item in toSentTo)
                {
                    var bytes = Encoding.Default.GetBytes(msg);
                    var arraySegment = new ArraySegment<byte>(bytes);
                    item.WebSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
            }
        }

    }

    public class SocketConnection
    {
        public Guid Id { get; set; }
        public WebSocket WebSocket { get; set; }
    }
}
