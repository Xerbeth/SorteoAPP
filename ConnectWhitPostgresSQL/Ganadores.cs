using ServicioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWhitPostgresSQL
{
    public class Ganadores
    {
        private ConnectionPgSQL connection;
        private Transaccion transaccion;

        public Ganadores(){ }

        /// <summary>
        /// Método para asignar premios a las personas
        /// </summary>
        /// <returns></returns>
        public Transaccion GenerateLottery()
        {
            connection = new ConnectionPgSQL();
            transaccion = new Transaccion();

            transaccion.Message = connection.CallFucntion("Ganadores");            
            transaccion.Code = 200;
            return transaccion;
        }

        /// <summary>
        /// Método para consultar el listado de los ganadores
        /// </summary>
        /// <returns> Objecto de la transacción </returns>
        public Transaccion GetListGanadores()
        {
            connection = new ConnectionPgSQL();

            Transaccion transaccion = new Transaccion();

            string query = "SELECT ROW_NUMBER() OVER(ORDER BY p.idpersona) AS Item, " +
                            "p.tipodocumento AS TipoDocumento, " +
                            "p.numerodocumento AS NumeroDocumento, " +
                            "p.primernombre || ' ' || p.segundonombre || ' ' || p.primerapellido || ' ' || p.segundoapellido AS Nombre, " +
                            "i.descripcion AS Articulo, " +
                            "COUNT(i.descripcion) AS Cantidad " +
                            "FROM \"ganadores\" g " +
                            "INNER JOIN personas p ON p.idpersona = g.idpersona " +
                            "INNER JOIN inventariopremios i ON i.idinventariopremio = g.idinventariopremio " +
                            "GROUP BY p.idpersona, p.TipoDocumento, p.numerodocumento, i.descripcion " +
                            "ORDER BY p.numerodocumento";

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
