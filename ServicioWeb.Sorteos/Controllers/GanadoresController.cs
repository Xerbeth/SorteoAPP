using ServicioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioWeb.Sorteos.Controllers
{
    public class GanadoresController : ApiController
    {

        public ConnectWhitPostgresSQL.Ganadores ganadoresBD;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GenerateLottery()
        {
            Transaccion transaccion = new Transaccion();
            ganadoresBD = new ConnectWhitPostgresSQL.Ganadores();           

            transaccion = ganadoresBD.GenerateLottery();

            return Request.CreateResponse(HttpStatusCode.Created, transaccion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Transaccion GetListGanadores()
        {
            Transaccion transaccion = new Transaccion();
            ganadoresBD = new ConnectWhitPostgresSQL.Ganadores();

            transaccion = ganadoresBD.GetListGanadores();

            return transaccion;
        }

    }
}
