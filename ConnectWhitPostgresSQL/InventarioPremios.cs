using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWhitPostgresSQL
{
    public class InventarioPremios
    {

        private ConnectionPgSQL connection;

        public InventarioPremios() { }

        /// <summary>
        /// método para consultar el lista del inventario de premios
        /// </summary>
        public string GetListInventarioPremios()
        {
            connection = new ConnectionPgSQL();

            string query = "SELECT IdInventarioPremio AS IdInventarioPremio,"+
                            " Descripcion AS Descripcion,"+
                            " Cantidad AS Cantidad,"+
                            " FechaRegistro AS FechaRegistro"+
                            " FROM \"inventariopremios\"";

            return connection.ExecQuery(query);

        }        

    }
}
