using System;
using System.Web;

namespace firstLab
{
    public class task1 : IHttpHandler
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
            string parmA = context.Request.QueryString.Get("ParmA");
            string parmB = context.Request.QueryString.Get("ParmB");

            string result = String.Format("GET-Http-MDI: ParmA = {0}, ParmB = {1} \n", parmA, parmB);

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = 200;
            context.Response.Write(result);
        }

        #endregion
    }
}
