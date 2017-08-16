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

namespace SistemaGestionNovedadesColombia.Personal
{
    public partial class Personal : Form
    {
        private ConexionSQL conexionSql;
        private bool modificar;

        public Personal(string tipo, string idVendedor)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = txtID;
            initComponents(tipo, idVendedor);
        }

        private void initComponents(string tipo, string idVendedor)
        {
            txtID.MaxLength = 13;
            txtTelefono.MaxLength = 10;

            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 45;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 45;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                if (!idVendedor.Equals("") && tipo.Equals("Modificar"))
                {
                    txtID.Text = idVendedor;
                    modificar = true;
                    fillForm();
                    txtID.Enabled = false;
                    txtNombre.Enabled = false;
                }
                if (tipo.Equals("Registrar"))
                {
                    comboStatus.SelectedIndex = 0;
                }
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else if (tipo.Equals("Consultar"))
            {
                if (!idVendedor.Equals(""))
                {
                    txtID.Text = idVendedor;
                    fillForm();
                }

                comboStatus.Enabled = false;
                txtID.Enabled = false;
                txtNombre.Enabled = false;
                txtDireccion.Enabled = false;
                txtTelefono.Enabled = false;
                numericDesdeN1.Enabled = false;
                numericDesdeN2.Enabled = false;
                numericDesdeN3.Enabled = false;
                numericDesdeN4.Enabled = false;
                numericDesdeN5.Enabled = false;
                numericHastaN1.Enabled = false;
                numericHastaN2.Enabled = false;
                numericHastaN3.Enabled = false;
                numericHastaN4.Enabled = false;
                numericHastaN5.Enabled = false;
                numericPorcentajeN1.Enabled = false;
                numericPorcentajeN2.Enabled = false;
                numericPorcentajeN3.Enabled = false;
                numericPorcentajeN4.Enabled = false;
                numericPorcentajeN5.Enabled = false;
                tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[3].Height = 10;
                btnSalir1.Height = 31;
            }
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from VENDEDOR where IDVENDEDOR = '" + txtID.Text + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtNombre.Text = reader.GetString(1);
                comboStatus.SelectedIndex = Convert.ToInt32(reader.GetBoolean(4));
                numericDesdeN1.Value = reader.GetDecimal(5);
                numericDesdeN2.Value = reader.GetDecimal(8);
                numericDesdeN3.Value = reader.GetDecimal(11);
                numericDesdeN4.Value = reader.GetDecimal(14);
                numericDesdeN5.Value = reader.GetDecimal(17);
                numericHastaN1.Value = reader.GetDecimal(6);
                numericHastaN2.Value = reader.GetDecimal(9);
                numericHastaN3.Value = reader.GetDecimal(12);
                numericHastaN4.Value = reader.GetDecimal(15);
                numericHastaN5.Value = reader.GetDecimal(18);
                numericPorcentajeN1.Value = reader.GetDecimal(7);
                numericPorcentajeN2.Value = reader.GetDecimal(10);
                numericPorcentajeN3.Value = reader.GetDecimal(13);
                numericPorcentajeN4.Value = reader.GetDecimal(16);
                numericPorcentajeN5.Value = reader.GetDecimal(19);
                if (!reader.IsDBNull(2))
                {
                    txtTelefono.Text = reader.GetString(2);
                }
                if (!reader.IsDBNull(3))
                {
                    txtDireccion.Text = reader.GetString(3);
                }
            }
            conexionSql.Desconectar();
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtID.Text))
                err += "-->No. de Identificación\n";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                err += "-->Nombre\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }
            {
                if (txtID.Text.Length == 10)
                {
                    if (Validacion.validarCedula(txtID.Text))
                    {
                        temp = validarTelefono();
                    }
                    else
                    {
                        MessageBox.Show("Cédula Incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtID.Clear();
                        temp = false;
                    }
                }
                else if (txtID.Text.Length == 13)
                {
                    if (Validacion.validarCedula(txtID.Text.Substring(0, 10)))
                    {
                        temp = validarTelefono();
                    }
                    else
                    {
                        MessageBox.Show("RUC Incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtID.Clear();
                        temp = false;
                    }
                }
                else if (txtID.Text.Length < 13 && txtID.Text.Length > 10)
                {
                    MessageBox.Show("RUC Incompleto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtID.Clear();
                    temp = false;
                }
                else
                {
                    temp = validarTelefono();
                }
            }
            return temp;
        }

        private bool validarTelefono()
        {
            if (txtTelefono.Text.Length >= 7)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Teléfono Dígitos Incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Clear();
                return false;
            }
        }

        private void guardarVendedor(string procedure)
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(procedure, conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IDVENDEDOR", SqlDbType.VarChar).Value = txtID.Text;
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = txtNombre.Text;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = comboStatus.SelectedIndex;
            cmd.Parameters.Add("@N1DESDE", SqlDbType.Money).Value = numericDesdeN1.Value;
            cmd.Parameters.Add("@N2DESDE", SqlDbType.Money).Value = numericDesdeN2.Value;
            cmd.Parameters.Add("@N3DESDE", SqlDbType.Money).Value = numericDesdeN3.Value;
            cmd.Parameters.Add("@N4DESDE", SqlDbType.Money).Value = numericDesdeN4.Value;
            cmd.Parameters.Add("@N5DESDE", SqlDbType.Money).Value = numericDesdeN5.Value;
            cmd.Parameters.Add("@N1HASTA", SqlDbType.Money).Value = numericHastaN1.Value;
            cmd.Parameters.Add("@N2HASTA", SqlDbType.Money).Value = numericHastaN2.Value;
            cmd.Parameters.Add("@N3HASTA", SqlDbType.Money).Value = numericHastaN3.Value;
            cmd.Parameters.Add("@N4HASTA", SqlDbType.Money).Value = numericHastaN4.Value;
            cmd.Parameters.Add("@N5HASTA", SqlDbType.Money).Value = numericHastaN5.Value;
            cmd.Parameters.Add("@N1PORCENTAJE", SqlDbType.Decimal).Value = numericPorcentajeN1.Value;
            cmd.Parameters.Add("@N2PORCENTAJE", SqlDbType.Decimal).Value = numericPorcentajeN2.Value;
            cmd.Parameters.Add("@N3PORCENTAJE", SqlDbType.Decimal).Value = numericPorcentajeN3.Value;
            cmd.Parameters.Add("@N4PORCENTAJE", SqlDbType.Decimal).Value = numericPorcentajeN4.Value;
            cmd.Parameters.Add("@N5PORCENTAJE", SqlDbType.Decimal).Value = numericPorcentajeN5.Value;
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                cmd.Parameters.Add("@TELEFONO", SqlDbType.VarChar).Value = txtTelefono.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                cmd.Parameters.Add("@DIRECCION", SqlDbType.VarChar).Value = txtDireccion.Text;
            }

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                if (modificar)
                {
                    guardarVendedor("actualizarVendedor");
                    MessageBox.Show("Vendedor modificado con éxito.", "Modificar Vendedor", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    conexionSql.Conectar();
                    SqlCommand cmd = new SqlCommand("select * from VENDEDOR where IDVENDEDOR = '" + txtID.Text + "'", conexionSql.getConnection());
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        guardarVendedor("registrarVendedor");
                        conexionSql.Desconectar();
                        MessageBox.Show("Vendedor registrado con éxito.", "Registrar Vendedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Vendedor ya se encuentra registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLimpiar.PerformClick();
                        conexionSql.Desconectar();
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!modificar)
            {
                txtID.Clear();
                txtNombre.Clear();
            }
            comboStatus.SelectedIndex = 0;
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            numericDesdeN1.Value = 0;
            numericDesdeN2.Value = 0;
            numericDesdeN3.Value = 0;
            numericDesdeN4.Value = 0;
            numericDesdeN5.Value = 0;
            numericHastaN1.Value = 0;
            numericHastaN2.Value = 0;
            numericHastaN3.Value = 0;
            numericHastaN4.Value = 0;
            numericHastaN5.Value = 0;
            numericPorcentajeN1.Value = 0;
            numericPorcentajeN2.Value = 0;
            numericPorcentajeN3.Value = 0;
            numericPorcentajeN4.Value = 0;
            numericPorcentajeN5.Value = 0;
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
