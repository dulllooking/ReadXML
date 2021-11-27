using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace ReadXML.Controllers
{
    public class ReadHtmlController : Controller
    {
        // GET: ReadHtml
        public ActionResult Index()
        {
            string strHtml = GetWebContent("https://www.kcg.gov.tw/Organ_Detail.aspx?n=D33B55D537402BAA&sms=9F779BBA07F163E2&s=CB882833D3848EBC");
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(strHtml.Replace(@"\r\n",""));
            HtmlNode node = document.DocumentNode.SelectSingleNode("//*[@id=\"data_midlle\"]/div[1]/div/table/tbody");

            return View(node);
        }

        //取得網頁資料內容
        private static string GetWebContent(string Url)
        {
            var request = WebRequest.Create(Url) as HttpWebRequest;
            WebClient wc = new WebClient(); //從 URI 所識別的資源中，傳送與接收資料
            //REF: https://stackoverflow.com/a/39534068/288936
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string res = wc.DownloadString(Url);
            // If required by the server, set the credentials.
            request.UserAgent = "PostmanRuntime/7.26.5";
            request.Accept = "*";
            request.Credentials = CredentialCache.DefaultCredentials;
            //驗證服務器證書回調自動驗證
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            // (重點是修改這行)set the security protocol before issuing the web request
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        //驗證伺服器憑證
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

    }
}