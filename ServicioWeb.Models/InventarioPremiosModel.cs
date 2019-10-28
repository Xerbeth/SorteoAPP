#region Documentacion general clase
/***********************************************************
 * Fecha:       28 de octubre de 2019
 * Autor:       Faiber Torres
 * Version:     1.0
 **********************************************************/
#endregion
#region Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace ServicioWeb.Models
{
    /// <summary>
    /// Clase para el mapeo y transporte de informacion para la entidad InventarioPremios 
    /// de la base de datos
    /// </summary>
    public class InventarioPremiosModel
    {
        public int IdInventarioPremio { set; get; }

        public string Descripcion { set; get; }

        public int Cantidad { set; get; }

        public string Fecharegistro { set; get; }

        /// <summary>
        /// Método constructor
        /// </summary>
        public InventarioPremiosModel() { }

    }
}
