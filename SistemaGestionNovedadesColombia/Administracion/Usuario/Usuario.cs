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
    public partial class Usuario : Form
    {
        private ConexionSQL conexionSql;
        private bool modificar;
        private string user;

        public Usuario(string tipo, string usuario)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = txtUsuario;
            initComponents(tipo, usuario);
        }

        private void initComponents(string tipo, string usuario)
        {
            btnSalir.Height = 31;
            btnLimpiar.Height = 31;
            btnGuardar.Height = 31;

            if (tipo.Equals("Registrar"))
            {
                chkAdministracionE.Enabled = false;
                chkClienteE.Enabled = false;
                chkFacturaE.Enabled = false;
                chkInventarioE.Enabled = false;
                chkVendedorE.Enabled = false;
                chkProveedorE.Enabled = false;
                comboEstado.SelectedIndex = 0;
            }
            else
            {
                modificar = true;
                user = usuario;
                txtUsuario.Text = usuario;
                fillForm();
            }
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
                comboEstado.SelectedIndex = Convert.ToInt32(reader.GetBoolean(16));
                chkClienteL.Checked = reader.GetBoolean(3);
                chkClienteE.Checked = reader.GetBoolean(4);
                chkProveedorL.Checked = reader.GetBoolean(5);
                chkProveedorE.Checked = reader.GetBoolean(6);
                chkInventarioL.Checked = reader.GetBoolean(7);
                chkInventarioE.Checked = reader.GetBoolean(8);
                chkFacturaL.Checked = reader.GetBoolean(9);
                chkFacturaE.Checked = reader.GetBoolean(10);
                chkVendedorL.Checked = reader.GetBoolean(11);
                chkVendedorE.Checked = reader.GetBoolean(12);
                chkAdministracionL.Checked = reader.GetBoolean(13);
                chkAdministracionE.Checked = reader.GetBoolean(14);
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

        private void guardarUsuario(string procedure)
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(procedure, conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USUARIOANT", SqlDbType.VarChar).Value = user;
            cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = txtUsuario.Text;
            cmd.Parameters.Add("@CONTRASENIA", SqlDbType.VarChar).Value = txtContrasenia.Text;
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = txtNombre.Text;
            cmd.Parameters.Add("@CLIENTEL", SqlDbType.Bit).Value = chkClienteL.Checked;
            cmd.Parameters.Add("@CLIENTEE", SqlDbType.Bit).Value = chkClienteE.Checked;
            cmd.Parameters.Add("@PROVEEDORL", SqlDbType.Bit).Value = chkProveedorL.Checked;
            cmd.Parameters.Add("@PROVEEDORE", SqlDbType.Bit).Value = chkProveedorE.Checked;
            cmd.Parameters.Add("@INVENTARIOL", SqlDbType.Bit).Value = chkInventarioL.Checked;
            cmd.Parameters.Add("@INVENTARIOE", SqlDbType.Bit).Value = chkInventarioE.Checked;
            cmd.Parameters.Add("@FACTURAL", SqlDbType.Bit).Value = chkFacturaL.Checked;
            cmd.Parameters.Add("@FACTURAE", SqlDbType.Bit).Value = chkFacturaE.Checked;
            cmd.Parameters.Add("@VENDEDORL", SqlDbType.Bit).Value = chkVendedorL.Checked;
            cmd.Parameters.Add("@VENDEDORE", SqlDbType.Bit).Value = chkVendedorE.Checked;
            cmd.Parameters.Add("@ADMINISTRACIONL", SqlDbType.Bit).Value = chkAdministracionL.Checked;
            cmd.Parameters.Add("@ADMINISTRACIONE", SqlDbType.Bit).Value = chkAdministracionE.Checked;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = comboEstado.SelectedIndex;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            txtNombre.Text = "";
            comboEstado.SelectedIndex = 0;
            chkClienteL.Checked = false;
            chkClienteE.Checked = false;
            chkProveedorL.Checked = false;
            chkProveedorE.Checked = false;
            chkInventarioL.Checked = false;
            chkInventarioE.Checked = false;
            chkFacturaL.Checked = false;
            chkFacturaE.Checked = false;
            chkVendedorL.Checked = false;
            chkVendedorE.Checked = false;
            chkAdministracionL.Checked = false;
            chkAdministracionE.Checked = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
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
                    if (modificar)
                    {
                        guardarUsuario("modifyUserAdmin");
                        MessageBox.Show("Usuario modificado con éxito.", "Usuario", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        guardarUsuario("addUser");
                        conexionSql.Desconectar();
                        MessageBox.Show("Usuario registrado con éxito.", "Usuario", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario ya se encuentra registrado.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    btnLimpiar.PerformClick();
                    conexionSql.Desconectar();
                }
            }
        }

        private void chkClienteL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClienteL.Checked)
            {
                chkClienteE.Enabled = true;
            }
            else
            {
                chkClienteE.Enabled = false;
                chkClienteE.Checked = false;
            }
        }

        private void chkProveedorL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProveedorL.Checked)
            {
                chkProveedorE.Enabled = true;
            }
            else
            {
                chkProveedorE.Enabled = false;
                chkProveedorE.Checked = false;
            }
        }

        private void chkInventarioL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInventarioL.Checked)
            {
                chkInventarioE.Enabled = true;
            }
            else
            {
                chkInventarioE.Enabled = false;
                chkInventarioE.Checked = false;
            }
        }

        private void chkFacturaL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFacturaL.Checked)
            {
                chkFacturaE.Enabled = true;
            }
            else
            {
                chkFacturaE.Enabled = false;
                chkFacturaE.Checked = false;
            }
        }

        private void chkVendedorL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVendedorL.Checked)
            {
                chkVendedorE.Enabled = true;
            }
            else
            {
                chkVendedorE.Enabled = false;
                chkVendedorE.Checked = false;
            }
        }

        private void chkAdministracionL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdministracionL.Checked)
            {
                chkAdministracionE.Enabled = true;
                chkAdministracionE.Checked = true;
            }
            else
            {
                chkAdministracionE.Enabled = false;
                chkAdministracionE.Checked = false;
            }
        }
    }
}
