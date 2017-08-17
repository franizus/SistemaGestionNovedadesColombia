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

namespace SistemaGestionNovedadesColombia.Facturacion
{
    public partial class EmitirFactura : Form
    {
        private ConexionSQL conexionSql;
        private int factura;

        public EmitirFactura(string tipo, int fact)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            factura = fact;
            this.ActiveControl = btnBuscarCliente;
            initComponents(tipo);
        }

        private void initComponents(string tipo)
        {
            gridViewFactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewFactura.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (tipo.Equals("Emitir"))
            {
                txtNumero.Enabled = true;
            }
            else
            {
                btnConsultar.Text = "Imprimir";
            }
            
            fillForm();
            fillGridView();

            btnBuscarCliente.Enabled = false;
            btnBuscarVendedor.Enabled = false;
            txtObservacion.Enabled = false;
            gridViewFactura.Enabled = false;
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from FACTURA where IDFACTURA = '" + factura + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtCliente.Text = reader.GetString(2);
                txtVendedor.Text = reader.GetString(1);
                lblTotal.Text = decimal.Round(reader.GetDecimal(8), 2, MidpointRounding.AwayFromZero).ToString();
                lblIVA.Text = decimal.Round(reader.GetDecimal(10), 2, MidpointRounding.AwayFromZero).ToString();
                lblSubT.Text = decimal.Round(reader.GetDecimal(9), 2, MidpointRounding.AwayFromZero).ToString();
                dateTimeHoy.Value = reader.GetDateTime(3);
                if (!reader.IsDBNull(6))
                {
                    txtNumero.Text = reader.GetString(6);
                }
                if (!reader.IsDBNull(7))
                {
                    txtObservacion.Text = reader.GetString(7);
                }
            }
            conexionSql.Desconectar();
            txtCliente.Text = getNomCliente(txtCliente.Text);
            txtVendedor.Text = getNomVendedor(txtVendedor.Text);
        }

        private DataTable fillDataTable()
        {
            string query = "select * from DETALLEFACTURA where IDFACTURA = '" + factura + "'";
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(query, conexionSql.getConnection());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        private void fillGridView()
        {
            DataTable dt = fillDataTable();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                gridViewFactura.Rows.Add(1);
                gridViewFactura.Rows[i].Cells[0].Value = row.ItemArray[2].ToString();
                gridViewFactura.Rows[i].Cells[1].Value = row.ItemArray[1].ToString();
                gridViewFactura.Rows[i].Cells[2].Value = row.ItemArray[4].ToString();
                gridViewFactura.Rows[i].Cells[3].Value = row.ItemArray[5].ToString();
                gridViewFactura.Rows[i].Cells[4].Value = row.ItemArray[6].ToString();
                i++;
            }
        }

        private string getNomVendedor(string idVend)
        {
            string refe = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select NOMBRE from VENDEDOR where IDVENDEDOR = '" + idVend + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private string getNomCliente(string idClie)
        {
            string refe = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select NOMBRE from CLIENTE where IDCLIENTE = '" + idClie + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private void guardarFactura()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("actualizarEmisionFact", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IDFACTURA", SqlDbType.VarChar).Value = factura;
            cmd.Parameters.Add("@NUMEROFACT", SqlDbType.VarChar).Value = txtNumero.Text;
            cmd.Parameters.Add("@EMITIDO", SqlDbType.Bit).Value = 1;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
                err += "-->Número\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }

            return temp;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (btnConsultar.Text.Equals("Imprimir"))
            {
                Imprimit im = new Imprimit();
                im.Show();
            }
            else
            {
                if (validarRegistro())
                {
                    guardarFactura();
                    if (MessageBox.Show("Factura emitida con éxito\n¿Desea imprimir la factura?", "Imprimir",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Imprimit im = new Imprimit();
                        im.Show();
                    }
                    else
                    {
                        this.Close();
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

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
