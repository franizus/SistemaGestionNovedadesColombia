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
    public partial class Cliente : Form
    {
        private ConexionSQL conexionSql;
        private bool modificar;

        public Cliente(string tipo, string idCliente)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = comboIDType;
            initComponents(tipo, idCliente);
        }

        private void fillComboProvincia()
        {
            conexionSql.Conectar();
            string query = "select PROVINCIA from ZONA group by PROVINCIA";
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboProvincia.Items.Add(sqlReader["PROVINCIA"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private void fillComboCiudad()
        {
            comboCiudad.Items.Clear();
            conexionSql.Conectar();
            string query = "select CIUDAD from ZONA where PROVINCIA = '" + comboProvincia.SelectedItem.ToString() + "' order by CIUDAD";
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();

            while (sqlReader.Read())
            {
                comboCiudad.Items.Add(sqlReader["CIUDAD"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
            comboCiudad.SelectedIndex = 0;
        }

        private void initComponents(string tipo, string idCliente)
        {
            txtID.MaxLength = 13;
            txtTelf.MaxLength = 10;
            fillComboProvincia();

            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 45;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 45;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                if (!idCliente.Equals("") && tipo.Equals("Modificar"))
                {
                    txtID.Text = idCliente;
                    modificar = true;
                    fillForm();
                    txtID.Enabled = false;
                    txtNombre.Enabled = false;
                }
                if (tipo.Equals("Registrar"))
                {
                    comboIDType.SelectedIndex = 0;
                    comboProvincia.SelectedIndex = 0;
                    fillComboCiudad();
                }

                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else
            {
                if (!idCliente.Equals(""))
                {
                    txtID.Text = idCliente;
                    fillForm();
                }

                comboIDType.Enabled = false;
                comboProvincia.Enabled = false;
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
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from ClienteYZona where IDCLIENTE = '" + txtID.Text + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtNombre.Text = reader.GetString(2);
                txtDireccion.Text = reader.GetString(6);
                comboIDType.SelectedItem = reader.GetString(1);
                comboProvincia.SelectedItem = reader.GetString(8);
                fillComboCiudad();
                comboCiudad.SelectedItem = reader.GetString(7);
                if (!reader.IsDBNull(3))
                {
                    txtContacto.Text = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    txtTelf.Text = reader.GetString(4);
                }
                if (!reader.IsDBNull(5))
                {
                    txtEmail.Text = reader.GetString(5);
                }
            }
            conexionSql.Desconectar();
        }

        private int getIDZona()
        {
            string provincia = comboProvincia.SelectedItem.ToString();
            string ciudad = comboCiudad.SelectedItem.ToString();
            int idZona = 0;

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select IDZONA from ZONA where PROVINCIA = '" + provincia + "' AND CIUDAD = '" + ciudad + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                idZona = reader.GetInt32(0);
            }
            conexionSql.Desconectar();

            return idZona;
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtID.Text))
                err += "-->No. de Identificación\n";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                err += "-->Nombre\n";
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                err += "-->Dirección\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Cédula Incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtID.Clear();
                        temp = false;
                    }
                }
                else if (txtID.Text.Length < 10 && comboIDType.SelectedIndex == 1)
                {
                    MessageBox.Show("Cédula Incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("RUC Incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtID.Clear();
                        temp = false;
                    }
                }
                else if (txtID.Text.Length < 13 && comboIDType.SelectedIndex == 0)
                {
                    MessageBox.Show("RUC Incompleto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!validarCorreo() || !validarTelefono())
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
                MessageBox.Show("E-mail Incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Teléfono Dígitos Incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelf.Clear();
                return false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!modificar)
            {
                comboIDType.SelectedIndex = 0;
                txtID.Clear();
                txtNombre.Clear();
            }
            comboProvincia.SelectedIndex = 0;
            fillComboCiudad();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtTelf.Clear();
            txtContacto.Clear();
        }

        private void guardarCliente(string procedure)
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(procedure, conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IDCLIENTE", SqlDbType.VarChar).Value = txtID.Text;
            cmd.Parameters.Add("@IDZONA", SqlDbType.Int).Value = getIDZona();
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
        }

        private void eliminarCliente()
        {
            conexionSql.Conectar();
            SqlCommand comando = new SqlCommand("delete from CLIENTE where IDCLIENTE = '" + txtID.Text + "'", conexionSql.getConnection());
            comando.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                if (modificar)
                {
                    guardarCliente("actualizarCliente");
                    MessageBox.Show("Cliente modificado con éxito.", "Modificar Cliente", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    conexionSql.Conectar();
                    SqlCommand cmd = new SqlCommand("select * from CLIENTE where IDCLIENTE = '" + txtID.Text + "'", conexionSql.getConnection());
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        guardarCliente("registrarCliente");
                        conexionSql.Desconectar();
                        MessageBox.Show("Cliente registrado con éxito.", "Registrar Cliente", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cliente ya se encuentra registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLimpiar.PerformClick();
                        conexionSql.Desconectar();
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir2_Click_1(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar?", "Eliminar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminarCliente();
                MessageBox.Show("Cliente eliminado con éxito.", "Eliminar", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modificar)
            {
                txtID.Enabled = true;
                if (comboIDType.SelectedIndex == 1)
                {
                    txtID.Text = txtID.Text.Substring(0, 10);
                }
                else if (comboIDType.SelectedIndex == 2)
                {
                    txtID.Text = "";
                }
            }

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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboProvincia_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            fillComboCiudad();
        }
    }
}
