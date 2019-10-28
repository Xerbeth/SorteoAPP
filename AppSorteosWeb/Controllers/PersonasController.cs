using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ServicioWeb.Models;

namespace AppSorteosWeb.Controllers
{
    public class PersonasController : Controller
    {

        // GET: Personas
        public ActionResult Index()
        {
            string urlApi = ConfigurationManager.AppSettings["UrlAPI"];
            string url = $"{urlApi}Personas/GetListPersonas";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            List<Personas> personas = new List<Personas>();

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                var result = streamReader.ReadToEnd();
                Transaccion trans = JsonConvert.DeserializeObject<Transaccion>(result);
                personas = JsonConvert.DeserializeObject<List<Personas>>(trans.Response.ToString());
            }
            return View("Personas", personas);
        }

        /// <summary>
        /// Método para registrar personas
        /// </summary>
        /// <param name="personas"> Modelo con la informacion respectiva de personas </param>
        /// <returns> Objecto con estado y respuesta de la transcación </returns>
        public ActionResult SavePersonas(Personas personas)
        {
            var json = new JavaScriptSerializer().Serialize(personas);
            byte[] dataStream = Encoding.UTF8.GetBytes(json);

            string urlApi = ConfigurationManager.AppSettings["UrlAPI"];
            string url = $"{urlApi}Personas/SavePersonas";
            
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = dataStream.Length;

            Stream newStream = request.GetRequestStream();
            // Send the data.
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Transaccion trans = JsonConvert.DeserializeObject<Transaccion>(responseString);

            return Json(trans, JsonRequestBehavior.AllowGet);
        }        

    }
}