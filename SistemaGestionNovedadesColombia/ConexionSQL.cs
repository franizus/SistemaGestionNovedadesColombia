using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionNovedadesColombia
{
    class ConexionSQL
    {
        private SqlConnection conexion;

        public void Conectar()
        {
            try
            {
                conexion = new SqlConnection("Data Source=DESKTOP-DC5VPQT\\SQLEXPRESS;Initial Catalog=SGIV;Integrated Security=True");
                conexion.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public SqlConnection getConnection()
        {
            return conexion;
        }

        public void Desconectar()
        {
            conexion.Close();
        }
    }
}
