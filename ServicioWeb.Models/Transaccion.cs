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
    /// Clase para transportar la información respectiva de la transacción 
    /// </summary>
    public class Transaccion
    {
        
        /// <summary>
        /// Código de la petición 
        /// </summary>
        public int Code { set; get; }

        /// <summary>
        /// Mensaje de la tranacción
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// Objecto de respuesta de la transacción
        /// </summary>
        public Object Response { set; get; }

        /// <summary>
        /// Método constructor
        /// </summary>
        public Transaccion()
        {
            this.Code = 400;
            this.Message = "Bad Request";
            this.Response = null;
        }

        /// <summary>
        /// Método constructor sobrecargado 
        /// permite asignar un código y mensaje personalizado 
        /// acorde al error ocurrido
        /// </summary>
        /// <param name="codigo"> Código del error </param>
        public Transaccion(string message)
        {
            if (message.Contains("23505"))
            {
                this.Message = "No fue posible registrar la persona. El Número de documento ya existe en la base de datos.";
                this.Code = 23505;
            }
            else {
                this.Message = "Ocurrió un error inexperado!";
            }
            
                        
        }


    }
}
