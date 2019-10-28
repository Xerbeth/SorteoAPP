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
    /// Clase para el mapeo y transporte de informacion de las personas
    /// de la base de datos
    /// </summary>
    public class Personas
    {
        public int Item { set; get; }
        public string TipoDocumento { set; get; }
        public long NumeroDocumento { set; get; }
        public string PrimerNombre { set; get; }
        public string SegunoNombre { set; get; }
        public string PrimerApellido { set; get; }
        public string SegundoApellido { set; get; }
        public string Sexo { set; get; }
        public string FechaNacimiento { set; get; }

        public DateTime FechaRegistro { set; get; }

        /// <summary>
        /// Método constrcutor de la clase
        /// </summary>
        public Personas() { }

    }
}
