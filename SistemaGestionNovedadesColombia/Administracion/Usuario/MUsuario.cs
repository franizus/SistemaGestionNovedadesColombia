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

namespace SistemaGestionNovedadesColombia.Administracion.Usuario
{
    public partial class MUsuario : Form
    {
        private ConexionSQL conexionSql;
        private string usuario;

        public MUsuario(string user)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            txtUsuario.Text = user;
            fillForm();
            usuario = user;
            btnSalir.Height = 31;
            btnLimpiar.Height = 31;
            btnGuardar.Height = 31;
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from USUARIO where USUARIO = '" + txtUsuario.Text + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtNombre.Text = reader.GetString(2);
                txtContrasenia.WaterMark = "Ingrese nueva contraseña";
            }
            conexionSql.Desconectar();
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                err += "-->Usuario\n";
            if (string.IsNullOrWhiteSpace(txtContrasenia.Text))
                err += "-->Contraseña\n";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                err += "-->Nombre\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }

            return temp;
        }

        private void modificarUsuario()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("modifyUser", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USUARIOANT", SqlDbType.VarChar).Value = usuario;
            cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = txtUsuario.Text;
            cmd.Parameters.Add("@CONTRASENIA", SqlDbType.VarChar).Value = txtContrasenia.Text;
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = txtNombre.Text;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                conexionSql.Conectar();
                SqlCommand cmd = new SqlCommand("select * from USUARIO where USUARIO = '" + txtUsuario.Text + "'",
                    conexionSql.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    modificarUsuario();
                    conexionSql.Desconectar();
                    MessageBox.Show("Datos modificados con éxito.", "Usuario", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario ya se encuentra registrado.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtUsuario.Text = "";
                    conexionSql.Desconectar();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            txtNombre.Text = "";
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
