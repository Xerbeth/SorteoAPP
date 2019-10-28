using ConnectWhitPostgresSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioWeb.Sorteos.Controllers
{
    public class InventarioPremiosController : ApiController
    {

        private InventarioPremios InventarioPremios;

        /// <summary>
        /// Método para consultar el listado del inventario de premios
        /// </summary>
        /// <returns> Lista de invitario de premios </returns>
        [HttpGet]
        public string GetListInventarioPremios()
        {
            InventarioPremios = new InventarioPremios();

            string result = InventarioPremios.GetListInventarioPremios();
            return result;
        }

        /// <summary>
        /// Método para consultar un item del inventario de premios
        /// </summary>
        [HttpGet]
        public void GetInventarioPremios()
        {
            InventarioPremios = new InventarioPremios();          
        }

        /// <summary>
        /// Método para registrar un item en el inventario de premios
        /// </summary>
        [HttpPost]
        public void SetInventerioPremios()
        {
            InventarioPremios = new InventarioPremios();

        }





    }
}
