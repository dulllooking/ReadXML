using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ReadXML.Controllers
{
    public class ReadXMLController : Controller
    {
        // GET: ReadXML
        public ActionResult Index()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load ("http://tbike-data.tainan.gov.tw/Service/StationStatus/Xml");
            XmlNode root = xmlDocument.DocumentElement;
            XmlNodeList xmlNodes = root.ChildNodes; // 資料在第二層
            ViewBag.ChildNodes = xmlNodes; //ViewBag.後名稱自訂
            ViewBag.TitleOne = "Read XML"; //ViewBag.後名稱自訂
            return View();
        }

        // GET: ReadXML
        public ActionResult Index1()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("http://tbike-data.tainan.gov.tw/Service/StationStatus/Xml");
            XmlNode root = xmlDocument.DocumentElement;
            XmlNodeList nodes = root.ChildNodes; // 資料在第二層
            //XmlNode node = root.SelectSingleNode("\\");
            //root.ParentNode
            return View(nodes);
        }
    }
}