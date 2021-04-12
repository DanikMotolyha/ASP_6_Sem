using System;
using System.IO;
using System.Web;

namespace firstLab
{
    public class task5_6 : IHttpHandler
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
            string textFromFile = File.ReadAllText(@"d:\Study\лабараторные\03_02\Интернет_сервера_ASP\labs\01\LAB01\four.html");
            if (context.Request.HttpMethod == "GET")
            {
                context.Response.ContentType = "text/html";
                context.Response.Write(textFromFile);
            }
            if (context.Request.HttpMethod == "POST")
            {
                int parmA = 0;
                int parmB = 0;
                Int32.TryParse(context.Request.Form.Get("x"), out parmA);
                Int32.TryParse(context.Request.Form.Get("y"), out parmB);
                context.Response.ContentType = "text/plain";
                int mul = parmA + parmB;
                context.Response.Write("x + y = " + mul);
            }
        }

        #endregion
    }
}
