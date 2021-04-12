using System;
using System.Diagnostics;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;


namespace firstLab
{
    public class websockets : IHttpHandler
    {
        /// <summary>
        /// Вам потребуется настроить этот обработчик в файле Web.config вашего 
        /// веб-сайта и зарегистрировать его с помощью IIS, чтобы затем воспользоваться им.
        /// см. на этой странице: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region Члены IHttpHandler

        private WebSocket socket;

        private readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();


        public bool IsReusable
        {
            // Верните значение false в том случае, если ваш управляемый обработчик не может быть повторно использован для другого запроса.
            // Обычно значение false соответствует случаю, когда некоторые данные о состоянии сохранены по запросу.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Debug.WriteLine("1");
            Debug.WriteLine(context.IsWebSocketRequest);
            if (context.IsWebSocketRequest)
            {
                Debug.WriteLine("123");
                context.AcceptWebSocketRequest(WebSocketRequest);
            }
            else
            {
                string textFromFile = File.ReadAllText(@"d:\Study\лабараторные\03_02\Интернет_сервера_ASP\labs\01\LAB01\socket.html");
                if (context.Request.HttpMethod == "GET")
                {
                    context.Response.ContentType = "text/html";
                    context.Response.Write(textFromFile);
                }
            }
        }
        private async Task WebSocketRequest(AspNetWebSocketContext context)
        {
            Debug.WriteLine("2");
            Locker.EnterWriteLock();
            try
            {
                socket = context.WebSocket;
            }
            finally
            {
                Locker.ExitWriteLock();
            }

            while (true)
            {
                var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString()));
                Debug.WriteLine("3");
                Thread.Sleep(3000);

                try
                {
                    if (socket.State == WebSocketState.Open)
                    {
                        Debug.WriteLine("4");
                        await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }

                catch (ObjectDisposedException)
                {
                    Locker.EnterWriteLock();
                    try
                    {
                        socket.Abort();
                    }
                    finally
                    {
                        Locker.ExitWriteLock();
                    }
                }
            }
        }


        #endregion
    }
}
