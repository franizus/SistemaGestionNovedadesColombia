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
    public partial class Factura : Form
    {
        private ConexionSQL conexionSql;
        private Dictionary<string, DataTable> grids;
        private int factura;

        public Factura(string tipo, int fact)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            grids = new Dictionary<string, DataTable>();
            MaximizeBox = false;
            MinimizeBox = false;
            factura = fact;
            this.ActiveControl = btnBuscarCliente;
            initComponents(tipo);
        }

        private void initComponents(string tipo)
        {
            dateTimeHoy.Value = DateTime.Today;
            radioBtnFactura.Select();

            gridViewFactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewFactura.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 33.33F;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 33.33F;
            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[2].Height = 23.33F;

            if (tipo.Equals("Registrar"))
            {
                tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[3].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else
            {
                panel1.Enabled = false;
                contextMenuStrip1.Enabled = false;

                fillForm();
                fillGridView();

                btnBuscarCliente.Enabled = false;
                btnBuscarVendedor.Enabled = false;
                txtObservacion.Enabled = false;
                gridViewFactura.Enabled = false;
                tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[4].Height = 10;
                btnSalir1.Height = 31;
                btnImprimir.Height = 31;
            }
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
                radioBtnNota.Checked = Convert.ToInt32(reader.GetBoolean(4)) == 0 ? false : true;
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

        private void guardarSalida()
        {
            foreach (var grid in grids)
            {
                string referencia = grid.Key;
                foreach (DataRow row in grid.Value.Rows)
                {
                    conexionSql.Conectar();
                    SqlCommand cmd = new SqlCommand("actualizarEntrada", conexionSql.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = referencia;
                    cmd.Parameters.Add("@TALLA", SqlDbType.VarChar).Value = row.ItemArray[0].ToString();
                    cmd.Parameters.Add("@COLOR", SqlDbType.VarChar).Value = row.ItemArray[1].ToString();
                    cmd.Parameters.Add("@CANTIDAD", SqlDbType.Int).Value = Int32.Parse(row.ItemArray[2].ToString()) - Int32.Parse(row.ItemArray[3].ToString());

                    cmd.ExecuteNonQuery();
                    conexionSql.Desconectar();
                }
            }
        }

        private string getIDVendedor(string nomVend)
        {
            string refe = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select IDVENDEDOR from VENDEDOR where NOMBRE = '" + nomVend + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private string getIDCliente(string nomClie)
        {
            string refe = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select IDCLIENTE from CLIENTE where NOMBRE = '" + nomClie + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private int getIDFact()
        {
            int fact = 0;

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select IDFACTURA from FACTURA GROUP BY IDFACTURA HAVING IDFACTURA = MAX(IDFACTURA)", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                fact = reader.GetInt32(0);
            }
            conexionSql.Desconectar();

            return fact;
        }

        private void guardarFactura()
        {
            string idvend = getIDVendedor(txtVendedor.Text);
            string idclie = getIDCliente(txtCliente.Text);
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("registrarFactura", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IDVENDEDOR", SqlDbType.VarChar).Value = idvend;
            cmd.Parameters.Add("@IDCLIENTE", SqlDbType.VarChar).Value = idclie;
            cmd.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = dateTimeHoy.Value;
            cmd.Parameters.Add("@TIPO", SqlDbType.Bit).Value = radioBtnNota.Checked ? 1 : 0;
            cmd.Parameters.Add("@EMITIDO", SqlDbType.Bit).Value = 0;
            cmd.Parameters.Add("@OBSERVACION", SqlDbType.VarChar).Value = txtObservacion.Text;
            cmd.Parameters.Add("@PRECIOTOTAL", SqlDbType.Money).Value = Decimal.Parse(lblTotal.Text);
            cmd.Parameters.Add("@SUBTOTAL", SqlDbType.Money).Value = Decimal.Parse(lblSubT.Text);
            cmd.Parameters.Add("@IVA", SqlDbType.Decimal).Value = Decimal.Parse(lblIVA.Text);

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void guardarDetalle(int idFact)
        {
            foreach (DataGridViewRow row in gridViewFactura.Rows)
            {
                conexionSql.Conectar();
                SqlCommand cmd = new SqlCommand("registrarDetalleFactura", conexionSql.getConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@IDFACTURA", SqlDbType.Int).Value = idFact;
                cmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = row.Cells[1].Value.ToString();
                cmd.Parameters.Add("@CANTIDAD", SqlDbType.Int).Value = Int32.Parse(row.Cells[0].Value.ToString());
                cmd.Parameters.Add("@REFERENCIAF", SqlDbType.VarChar).Value = row.Cells[1].Value.ToString();
                cmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = row.Cells[2].Value.ToString();
                cmd.Parameters.Add("@PRECIOUNIT", SqlDbType.Money).Value = Decimal.Parse(row.Cells[3].Value.ToString());
                cmd.Parameters.Add("@PRECIOTOT", SqlDbType.Money).Value = Decimal.Parse(row.Cells[4].Value.ToString());

                cmd.ExecuteNonQuery();
                conexionSql.Desconectar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarCliente bc = new BuscarCliente(txtCliente);
            bc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BuscarVendedor bv = new BuscarVendedor(txtVendedor);
            bv.Show();
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtCliente.Text))
                err += "-->Nombre de Cliente\n";
            if (string.IsNullOrWhiteSpace(txtVendedor.Text))
                err += "-->Vendedor\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }

            return temp;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gridViewFactura.RowCount >= 1 && validarRegistro())
            {
                guardarFactura();
                guardarDetalle(getIDFact());
                guardarSalida();
                if (MessageBox.Show("Factura guardada con éxito\n¿Desea imprimir la factura?", "Imprimir",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnImprimir.PerformClick();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No se ha ingresado ninguna artículo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtVendedor.Text = "";
            txtObservacion.Text = "";
            lblTotal.Text = "0.00";
            lblIVA.Text = "0.00";
            lblSubT.Text = "0.00";
            panel1.Enabled = true;
            do
            {
                foreach (DataGridViewRow row in gridViewFactura.Rows)
                {
                    try
                    {
                        gridViewFactura.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (gridViewFactura.Rows.Count > 1);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void radioBtnNota_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnNota.Checked)
            {
                tableLayoutPanel8.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel8.RowStyles[1].Height = 0;
            }
            else
            {
                tableLayoutPanel8.RowStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel8.RowStyles[1].Height = 22;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            BuscarArticulo ba = new BuscarArticulo(grids, gridViewFactura, lblSubT, lblIVA, lblTotal, radioBtnFactura.Checked);
            ba.initGridView();
            ba.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grids.Remove(gridViewFactura.SelectedRows[0].Cells[1].Value.ToString());
            gridViewFactura.Rows.RemoveAt(gridViewFactura.SelectedRows[0].Index);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimit im = new Imprimit();
            im.Show();
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Factura_Load(object sender, EventArgs e)
        {
            
        }
    }
}
