#region Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServicioWeb.Models;
#endregion

namespace ServicioWeb.Sorteos.Controllers
{
    public class PersonasController : ApiController
    {

        public ConnectWhitPostgresSQL.Personas personasBD;


        /// <summary>
        /// Método para regitrar personas
        /// </summary>
        /// <param name="persona"> Modelo personas </param>
        /// <returns> Información de la transacción </returns>
        [HttpPost]
        public HttpResponseMessage SavePersonas(Personas persona)
        {
            Transaccion transaccion = new Transaccion();
            personasBD = new ConnectWhitPostgresSQL.Personas();
            transaccion  = personasBD.SavePersonas(persona);
            
            return Request.CreateResponse(HttpStatusCode.Created, transaccion);
        }

        /// <summary>
        /// Método para consultar el listado de las personas registradas en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Transaccion GetListPersonas()
        {

            Transaccion transaccion = new Transaccion();
            personasBD = new ConnectWhitPostgresSQL.Personas();

            transaccion = personasBD.GetListPersonas();

            return transaccion;
        }             

    }
}
