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
    public partial class RegistroCliente : Form
    {
        private ConexionSQL conexionSql;

        public RegistroCliente(String tipo)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = comboIDType;
            initComponents(tipo);
        }

        private void fillComboZona()
        {
            conexionSql.Conectar();
            string query = "select * from ZonaCiudadProvincia";
            SqlCommand sqlCmd = new SqlCommand("SELECT * FROM ZonaCiudadProvincia", conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();

            while (sqlReader.Read())
            {
                comboZona.Items.Add(sqlReader["Zona"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private void initComponents(String tipo)
        {
            txtID.MaxLength = 13;
            txtTelf.MaxLength = 10;
            fillComboZona();

            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 45;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 45;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else
            {
                comboIDType.Enabled = false;
                comboZona.Enabled = false;
                txtID.Enabled = false;
                txtNombre.Enabled = false;
                txtEmail.Enabled = false;
                txtDireccion.Enabled = false;
                txtTelf.Enabled = false;
                txtContacto.Enabled = false;
                if (tipo.Equals("Consultar"))
                {
                    tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[3].Height = 10;
                    btnSalir1.Height = 31;
                }
                if (tipo.Equals("Eliminar"))
                {
                    tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[4].Height = 10;
                    btnSalir2.Height = 31;
                    btnEliminar.Height = 31;
                }
            }

            comboIDType.SelectedIndex = 0;
            comboIDType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboZona.SelectedIndex = 0;
            comboZona.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Error Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtID.Text))
                err += "-->No. de Identificación\n";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                err += "-->Nombre\n";
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                err += "-->Dirección\n";

            if (!err.Equals("Error Campo Vacio :\n"))
            {
                MessageBox.Show(err);
                temp = false;
            }
            else
            {
                if (txtID.Text.Length == 10 && comboIDType.SelectedIndex == 1)
                {
                    if (Validacion.validarCedula(txtID.Text))
                    {
                        temp = validarCorreoYTelf();
                    }
                    else
                    {
                        MessageBox.Show("Error Cédula: Incorrecta");
                        txtID.Clear();
                        temp = false;
                    }
                }
                else if (txtID.Text.Length < 10 && comboIDType.SelectedIndex == 1)
                {
                    MessageBox.Show("Error Cédula: Dígitos Incompletos");
                    txtID.Clear();
                    temp = false;
                }
                else if (txtID.Text.Length == 13 && comboIDType.SelectedIndex == 0)
                {
                    if (Validacion.validarCedula(txtID.Text.Substring(0, 10)))
                    {
                        temp = validarCorreoYTelf();
                    }
                    else
                    {
                        MessageBox.Show("Error RUC: Incorrecta");
                        txtID.Clear();
                        temp = false;
                    }
                }
                else if (txtID.Text.Length < 12 && comboIDType.SelectedIndex == 0)
                {
                    MessageBox.Show("Error RUC: Dígitos Incompletos");
                    txtID.Clear();
                    temp = false;
                }
                else if (comboIDType.SelectedIndex == 2)
                {
                    temp = validarCorreoYTelf();
                }
            }
            return temp;
        }

        private bool validarCorreoYTelf()
        {
            bool temp = true;

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtTelf.Text))
            {
                if (!validarCorreo() && !validarTelefono())
                {
                    temp = false;
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtEmail.Text) && string.IsNullOrWhiteSpace(txtTelf.Text))
            {
                temp = validarCorreo();
            }
            else if (string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtTelf.Text))
            {
                temp = validarTelefono();
            }

            return temp;
        }

        private bool validarCorreo()
        {
            if (Validacion.validarEmail(txtEmail.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Error Email: Incorrecto");
                txtEmail.Clear();
                return false;
            }
        }

        private bool validarTelefono()
        {
            if (txtTelf.Text.Length >= 7)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Error Teléfono: Dígitos Incompletos");
                txtTelf.Clear();
                return false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            comboIDType.SelectedIndex = 0;
            comboZona.SelectedIndex = 0;
            txtID.Clear();
            txtNombre.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtTelf.Clear();
            txtContacto.Clear();
        }

        private void guardarCliente()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("registrarCliente", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IDCLIENTE", SqlDbType.VarChar).Value = txtID.Text;
            cmd.Parameters.Add("@IDZONA", SqlDbType.Int).Value = comboZona.SelectedIndex + 1;
            cmd.Parameters.Add("@TIPOID", SqlDbType.VarChar).Value = comboIDType.Text;
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = txtNombre.Text;
            cmd.Parameters.Add("@DIRECCION", SqlDbType.VarChar).Value = txtDireccion.Text;
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtEmail.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtTelf.Text))
            {
                cmd.Parameters.Add("@TELEFONO", SqlDbType.VarChar).Value = txtTelf.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtContacto.Text))
            {
                cmd.Parameters.Add("@CONTACTO", SqlDbType.VarChar).Value = txtContacto.Text;
            }

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
            MessageBox.Show("RegistroCliente guardado con exito.", "Registro RegistroCliente", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                conexionSql.Conectar();
                SqlCommand cmd = new SqlCommand("select * from CLIENTE where IDCLIENTE = '" + txtID + "'", conexionSql.getConnection());
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    guardarCliente();
                }
                else
                {
                    MessageBox.Show("Cliente ya se encuentra registrado.");
                    btnLimpiar.PerformClick();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void btnSalir2_Click_1(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RegistroCliente eliminado con exito.", "Eliminacion RegistroCliente", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void comboIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboIDType.SelectedIndex == 0)
            {
                txtID.MaxLength = 13;
            }
            else if (comboIDType.SelectedIndex == 1)
            {
                txtID.MaxLength = 10;
            }
            else
            {
                txtID.MaxLength = 0;
            }
        }

        private void txtTelf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
