using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace ConnectWhitPostgresSQL
{
    public class ConnectionPgSQL
    {

        private NpgsqlConnection CpgsqlConnection;

        /// <summary>
        /// Método constructor para acceso a la base de datos
        /// </summary>
        public ConnectionPgSQL()
        {
            CpgsqlConnection = new NpgsqlConnection("Server =  localhost; Port = 5432;User Id = postgres; Password = d3v3l0p3r; Database = Sorteos");
        }

        /// <summary>
        /// Método para crear la coneccion con el servidor 
        /// </summary>
        public void Connect()
        {
            CpgsqlConnection.Open();           
            
        }

        /// <summary>
        /// Método para cerrar las coneccion con el servidor
        /// </summary>
        public void Disconnect()
        {
            if (CpgsqlConnection.State == System.Data.ConnectionState.Open)
            {
                CpgsqlConnection.Close();             
            }            
            
        }

        public void InsertInventarioPremios()
        {
            string sql = "insert into inventariopremios(descripcion, cantidad)" +
            "values('Balon de futbol', 3)";


            NpgsqlCommand ejecutar = new NpgsqlCommand(sql, CpgsqlConnection);

            ejecutar.ExecuteNonQuery();
        }

        /// <summary>
        /// Método para registrar en la base de datos
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Object SaveQuery(string query)
        {
            NpgsqlCommand ejecutar = new NpgsqlCommand(query, CpgsqlConnection);

            return ejecutar.ExecuteNonQuery();
        } 

        /// <summary>
        /// Método para realizar consultas en la base de datos
        /// </summary>
        /// <param name="query"> Consulta a realizar </param>
        /// <returns></returns>
        public string ExecQuery(string query)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, CpgsqlConnection);
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(npgsqlCommand);
            DataTable resultQuery = new DataTable();
            npgsqlDataAdapter.Fill(resultQuery);                                          

            return JsonConvert.SerializeObject(resultQuery, Formatting.Indented);
        }

        /// <summary>
        /// Método para ejecutar funciones de postgres
        /// </summary>
        public string CallFucntion(string function)
        {
            this.Connect();
            
            // Define a command to call show_cities() procedure
            NpgsqlCommand command = new NpgsqlCommand(function, CpgsqlConnection);
            command.CommandType = CommandType.StoredProcedure;

            // Linea para mapear el parametro de respuesta de la function
            command.Parameters.Add(new NpgsqlParameter(":respuesta", NpgsqlDbType.Varchar)).Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();

            this.Disconnect();           
            return (string)command.Parameters[":respuesta"].Value; ;
        }

    }
    }
