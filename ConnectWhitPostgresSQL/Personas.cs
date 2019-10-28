using ServicioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWhitPostgresSQL
{
    /// <summary>
    /// Clase personas con las propiedades de la misma
    /// </summary>
    public class Personas
    {

        private ConnectionPgSQL connection;
        private Transaccion transaccion;
        /// <summary>
        /// Método constructor de la clase
        /// </summary>
        public Personas() { }

        /// <summary>
        /// método para registrar personas
        /// </summary>
        /// <returns> Objeto de transacción </returns>
        public Transaccion SavePersonas(ServicioWeb.Models.Personas persona) {

            connection = new ConnectionPgSQL();            

            string query =  "INSERT INTO Personas (TipoDocumento, NumeroDocumento, PrimerNombre, "+
                            "SegundoNombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento)"+
                            "VALUES( '"+ persona.TipoDocumento + "','"+
                                    persona.NumeroDocumento + "','" +
                                    persona.PrimerNombre + "','" +
                                    persona.SegunoNombre + "','" +
                                    persona.PrimerApellido + "','" +
                                    persona.SegundoApellido + "','" +
                                    persona.Sexo + "','" +
                                    persona.FechaNacimiento+"')";

            
            try
            {
                connection.Connect();
                connection.SaveQuery(query);
                transaccion = new Transaccion();
                transaccion.Code = 200;
                transaccion.Message = "Datos registrados correctamente!.";
            }
            catch (Exception ex)
            {
                transaccion = new Transaccion(ex.Message);
            }
            finally
            {
                connection.Disconnect();
            }            

            return transaccion;

        }

        /// <summary>
        /// Método para consultar el listado de personas registradas en la base de datos
        /// </summary>
        /// <returns> Lista de personas </returns>
        public Transaccion GetListPersonas()
        {
            connection = new ConnectionPgSQL();

            Transaccion transaccion = new Transaccion();

            string query = "SELECT ROW_NUMBER () OVER (ORDER BY numerodocumento) AS Item,"+
                            "tipodocumento AS TipoDocumento, " +
                            "numerodocumento AS NumeroDocumento, "+
		                    "primernombre AS PrimerNombre, "+
		                    "segundonombre AS SegundoNombre, "+
		                    "primerapellido AS PrimerApellido, "+
                            "segundoapellido AS SegundoApellido, "+
                            "sexo AS Sexo, "+
                            "fechanacimiento AS FechaNacimiento, "+
                            "fecharegistro AS FechaRegistro "+
                            "FROM \"personas\"";


            try
            {
                connection.Connect();
                transaccion.Response = connection.ExecQuery(query);
                transaccion.Code = 200;
                transaccion.Message = "Consulta realizada correctamente!.";
            }
            catch (Exception ex)
            {
                transaccion = new Transaccion(ex.Message);
            }
            finally
            {
                connection.Disconnect();
            }
            
            return transaccion;
        }

    }
}
