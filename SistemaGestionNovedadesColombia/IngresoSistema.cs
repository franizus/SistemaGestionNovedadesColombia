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

namespace SistemaGestionNovedadesColombia
{
    public partial class IngresoSistema : Form
    {
        private ConexionSQL conexionSql;

        public IngresoSistema()
        {
            InitializeComponent();
            this.CenterToScreen();
            conexionSql = new ConexionSQL();
        }

        private bool validarUsuario()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("userLogin", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar).Value = txtUsuario.Text;
            cmd.Parameters.Add("@CONTRASENIA", SqlDbType.NVarChar).Value = txtContrasenia.Text;
            SqlParameter retval = cmd.Parameters.Add("@responseMessage", SqlDbType.NVarChar);
            retval.Direction = ParameterDirection.Output;
            retval.Size = 250;
            cmd.ExecuteNonQuery();

            string retunvalue = (string)cmd.Parameters["@responseMessage"].Value;
            conexionSql.Desconectar();
            if (retunvalue.Equals("User successfully logged in"))
            {
                return true;
            }
            else if (retunvalue.Equals("Usuario inactivo"))
            {
                MessageBox.Show("Usuario inactivo.", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cleanTxt();
                return false;
            }
            else
            {
                MessageBox.Show("El usuario o contraseña son incorrectos.", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cleanTxt();
                return false;
            }
        }

        private void cleanTxt()
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContrasenia.Text) && !string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El campo de contraseña esta vacio.", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                cleanTxt();
                MessageBox.Show("El campo de usuario esta vacio.", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(txtContrasenia.Text) && string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                cleanTxt();
                MessageBox.Show("Campos vacios.", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (validarUsuario())
            {
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.setStatusBar(txtUsuario.Text);
                mw.obtenerPermisos();
                mw.initComponents();
                mw.Closed += (s, args) => this.Close();
                mw.Show();
            }
        }

        private void txtContrasenia_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
