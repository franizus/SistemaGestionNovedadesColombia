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
    public partial class BuscarArticulo : Form
    {
        private ConexionSQL conexionSql;
        private string referencia;
        private Dictionary<string, DataTable> grids;
        private DataGridView data;
        private Label subt, iva, tot;
        private bool check;

        public BuscarArticulo(Dictionary<string, DataTable> grid, DataGridView dat, Label s, Label i, Label t, bool c)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            grids = grid;
            data = dat;
            subt = s;
            iva = i;
            tot = t;
            check = c;
            MaximizeBox = false;
            MinimizeBox = false;
            comboBusqueda.SelectedIndex = 0;
        }

        public void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from vistaArticulo";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewArticulo.ReadOnly = true;
            gridViewArticulo.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewArticulo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewArticulo.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var bd = (BindingSource)gridViewArticulo.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewArticulo.Columns[2].DataPropertyName + " like '%{0}%'", "Activo");
            gridViewArticulo.Refresh();
        }

        private DataTable fillDataTable()
        {
            string query = "select * from vistaArticuloTallas where REFERENCIA = '" + referencia + "'";
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(query, conexionSql.getConnection());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        private void fillGridView()
        {
            DataTable dt = fillDataTable();
            gridViewSalida.Columns[0].Width = 90;
            gridViewSalida.Columns[1].Width = 90;
            gridViewSalida.Columns[2].Width = 90;
            gridViewSalida.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                gridViewSalida.Rows.Add(1);
                gridViewSalida.Rows[i].Cells[0].Value = row.ItemArray[0].ToString();
                gridViewSalida.Rows[i].Cells[1].Value = row.ItemArray[1].ToString();
                gridViewSalida.Rows[i].Cells[2].Value = row.ItemArray[2].ToString();
                i++;
            }

            gridViewSalida.Columns[0].ReadOnly = true;
            gridViewSalida.Columns[1].ReadOnly = true;
            gridViewSalida.Columns[2].ReadOnly = true;
            gridViewSalida.CurrentCell = gridViewSalida.Rows[0].Cells[3];
            gridViewSalida.BeginEdit(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            var bd = (BindingSource)gridViewArticulo.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewArticulo.Columns[comboBusqueda.SelectedIndex].DataPropertyName + " like '%{0}%'", txtBusqueda.Text.Trim().Replace("'", "''"));
            gridViewArticulo.Refresh();
        }

        private string getNombre()
        {
            string nom = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select NOMBREART from ARTICULO where REFERENCIA = '" + referencia + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nom = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return nom;
        }

        private decimal getPrecio()
        {
            decimal refe = 0;

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select PRECIOVENTA from ARTICULO where REFERENCIA = '" + referencia + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetDecimal(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarGuardar())
            {
                DataTable data1 = new DataTable();
                foreach (DataGridViewColumn col in gridViewSalida.Columns)
                {
                    data1.Columns.Add(col.HeaderText);
                }

                foreach (DataGridViewRow row in gridViewSalida.Rows)
                {
                    DataRow dRow = data1.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    data1.Rows.Add(dRow);
                }
                grids.Add(referencia, data1);
                data.Rows.Add(1);
                data.Rows[data.RowCount - 1].Cells[1].Value = referencia;
                int cantidad = sumarCantidad();
                data.Rows[data.RowCount - 1].Cells[0].Value = cantidad;
                data.Rows[data.RowCount - 1].Cells[2].Value = getNombre();
                decimal precio = getPrecio();
                data.Rows[data.RowCount - 1].Cells[3].Value = decimal.Round(precio, 2, MidpointRounding.AwayFromZero);
                data.Rows[data.RowCount - 1].Cells[4].Value = decimal.Round((cantidad * precio), 2, MidpointRounding.AwayFromZero);
                data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                data.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                decimal suma = 0;
                foreach (DataGridViewRow row in data.Rows)
                {
                    suma += Decimal.Parse(row.Cells[4].Value.ToString());
                }

                decimal subto = suma;
                subt.Text = decimal.Round(subto, 2, MidpointRounding.AwayFromZero).ToString();

                decimal iva1 = 0;
                if (check)
                {
                    iva1 = subto * getIVA();
                    iva.Text = decimal.Round(iva1, 2, MidpointRounding.AwayFromZero).ToString();
                }

                decimal total = subto + iva1;
                tot.Text = decimal.Round(total, 2, MidpointRounding.AwayFromZero).ToString();

                this.Close();
            }
            else
            {
                MessageBox.Show("El valor de salida no puede ser mayor que la cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal getIVA()
        {
            decimal fact = 0;

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select IVA from PARAMETROS where PARAMID = 1", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                fact = reader.GetDecimal(0);
            }
            conexionSql.Desconectar();

            return fact;
        }

        private int sumarCantidad()
        {
            int suma = 0;
            foreach (DataGridViewRow row in gridViewSalida.Rows)
            {
                suma += Int32.Parse(row.Cells[3].Value.ToString());
            }
            return suma;
        }

        private void gridViewArticulo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            referencia = gridViewArticulo.SelectedRows[0].Cells[0].Value.ToString();
            fillGridView();
        }

        private void gridViewSalida_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (gridViewArticulo.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool validarGuardar()
        {
            bool validar = true;
            foreach (DataGridViewRow row in this.gridViewSalida.Rows)
            {
                double value1;
                double value2;
                if (!double.TryParse(row.Cells[3].Value.ToString(), out value1) ||
                    !double.TryParse(row.Cells[2].Value.ToString(), out value2))
                {
                    // throw exception or other handling here for unexcepted values in cells
                }
                else if (value1 > value2)
                {
                    validar = false;
                }
            }

            return validar;
        }
    }
}
