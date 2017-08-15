using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Administracion.Parametros
{
    public partial class GrupoTallaColor : Form
    {
        private ConexionSQL conexionSql;
        private bool modificar;
        private string user;

        public GrupoTallaColor(string usuario)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            initComponents(usuario);
        }

        private void initComponents(string usuario)
        {
            btnSalir.Height = 31;
            btnLimpiar.Height = 31;
            btnGuardar.Height = 31;

            if (!usuario.Equals(""))
            {
                modificar = true;
                txtNombre.Text = usuario;
                user = usuario;
                fillForm();
            }
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from GRUPOTALLACOLOR where NOMBREGTC = '" + txtNombre.Text + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtTallas.Text = reader.GetString(1);
                txtColores.Text = reader.GetString(2);
            }
            conexionSql.Desconectar();
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                err += "-->Nombre\n";
            if (string.IsNullOrWhiteSpace(txtTallas.Text))
                err += "-->Tallas\n";
            if (string.IsNullOrWhiteSpace(txtColores.Text))
                err += "-->Colores\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }

            return temp;
        }

        private void guardarGTC()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("guardarGTC", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@NOMBREGTC", SqlDbType.VarChar).Value = txtNombre.Text;
            cmd.Parameters.Add("@TALLAS", SqlDbType.VarChar).Value = txtTallas.Text;
            cmd.Parameters.Add("@COLORES", SqlDbType.VarChar).Value = txtColores.Text;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void modificarGTC()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("modificarGTC", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@NOMBREGTCANT", SqlDbType.VarChar).Value = user;
            cmd.Parameters.Add("@NOMBREGTC", SqlDbType.VarChar).Value = txtNombre.Text;
            cmd.Parameters.Add("@TALLAS", SqlDbType.VarChar).Value = txtTallas.Text;
            cmd.Parameters.Add("@COLORES", SqlDbType.VarChar).Value = txtColores.Text;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                conexionSql.Conectar();
                SqlCommand cmd =
                    new SqlCommand("select * from GRUPOTALLACOLOR where NOMBREGTC = '" + txtNombre.Text + "'",
                        conexionSql.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    if (modificar)
                    {
                        modificarGTC();
                        MessageBox.Show("Modificado con éxito.",
                            "Grupo de Tallas y Colores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        guardarGTC();
                        conexionSql.Desconectar();
                        MessageBox.Show("Registrado con éxito.",
                            "Grupo de Tallas y Colores", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Nombre ya se encuentra registrado.", "Grupo de Tallas y Colores",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Text = "";
                    conexionSql.Desconectar();
                }
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtTallas.Text = "";
            txtColores.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
