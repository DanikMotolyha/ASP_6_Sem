using System;
using System.Web;

namespace firstLab
{
    public class task4 : IHttpHandler
    {
        /// <summary>
        /// Вам потребуется настроить этот обработчик в файле Web.config вашего 
        /// веб-сайта и зарегистрировать его с помощью IIS, чтобы затем воспользоваться им.
        /// см. на этой странице: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region Члены IHttpHandler

        public bool IsReusable
        {
            // Верните значение false в том случае, если ваш управляемый обработчик не может быть повторно использован для другого запроса.
            // Обычно значение false соответствует случаю, когда некоторые данные о состоянии сохранены по запросу.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string parmA = context.Request.Params.Get("x");
            string parmB = context.Request.Params.Get("y");
            int x;  Int32.TryParse(parmA, out x);
            int y;  Int32.TryParse(parmB, out y);
            Console.WriteLine(context.Request.Form.Get("y"));
            context.Response.ContentType = "text/plain";
            int sum = x + y;
            context.Response.Write("x + y = " + sum);
        }

        #endregion
    }
}
