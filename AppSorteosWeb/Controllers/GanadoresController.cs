using Newtonsoft.Json;
using ServicioWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppSorteosWeb.Controllers
{
    public class GanadoresController : Controller
    {
        // GET: Ganadores
        public ActionResult Index()
        {
            string urlApi = ConfigurationManager.AppSettings["UrlAPI"];
            string url = $"{urlApi}Ganadores/GetListGanadores";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            List<GanadoresModel> ganadores = new List<GanadoresModel>();

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Transaccion trans = JsonConvert.DeserializeObject<Transaccion>(result);
                ganadores = JsonConvert.DeserializeObject<List<GanadoresModel>>(trans.Response.ToString());
            }

            return View("Ganadores", ganadores);
        }

        /// <summary>
        /// Método para asignar los ganadores de los premios
        /// </summary>
        /// <returns> Objecto de la transacción </returns>
        [HttpPost]
        public ActionResult GenerateLottery()
        {          
            string urlApi = ConfigurationManager.AppSettings["UrlAPI"];
            string url = $"{urlApi}Ganadores/GenerateLottery";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = 0;

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Transaccion trans = JsonConvert.DeserializeObject<Transaccion>(responseString);

            return Json(trans, JsonRequestBehavior.AllowGet);           
        }
    }
}