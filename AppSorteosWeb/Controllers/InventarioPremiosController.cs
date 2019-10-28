using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ServicioWeb.Models;

namespace AppSorteosWeb.Controllers
{
    public class InventarioPremiosController : Controller
    {
        // GET: InventarioPremios
        public ActionResult Index()
        {

            string url = "http://localhost:58772/Api/InventarioPremios/GetListInventarioPremios";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            List<InventarioPremiosModel> inventarioPremiosModel = new List<InventarioPremiosModel>();

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                var result = streamReader.ReadToEnd();
                var jsonResult = JsonConvert.DeserializeObject(result).ToString();
                inventarioPremiosModel = JsonConvert.DeserializeObject<List<InventarioPremiosModel>>(jsonResult);                
            }

            return View("InventarioPremios",inventarioPremiosModel);
        }
    }
}