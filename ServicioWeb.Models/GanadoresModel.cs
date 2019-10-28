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
    /// Clase para el mapeo y transporte de la relación entre premios y personas
    /// </summary>
    public class GanadoresModel
    {
        public int Item { set; get; }

        public string TipoDocumento { set; get; }

        public string NumeroDocumento { set; get; }
        
        public string Nombre { set; get; }

        public string Articulo { set; get; }

        public int Cantidad { set; get; }

        public GanadoresModel() { }
    }


}
