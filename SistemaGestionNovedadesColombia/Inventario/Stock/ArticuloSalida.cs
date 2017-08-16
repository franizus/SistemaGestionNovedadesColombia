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

namespace SistemaGestionNovedadesColombia.Inventario.Stock
{
    public partial class ArticuloSalida : Form
    {
        private ConexionSQL conexionSql;
        private Dictionary<string, DataTable> grids;
        private DataGridView data;
        private string nombArt;

        public ArticuloSalida(string nomProv, Dictionary<string, DataTable> grid, DataGridView dat)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            grids = grid;
            data = dat;
            fillComboArticulo(nomProv);
            comboArticulo.SelectedIndex = 0;
        }

        private void fillComboArticulo(string nomProv)
        {
            conexionSql.Conectar();
            string query = "select NOMBREART from articuloProv where NOMBRE = '" + nomProv + "'";
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboArticulo.Items.Add(sqlReader["NOMBREART"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private string getReferencia(string nomArt)
        {
            string refe = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select REFERENCIA from ARTICULO where NOMBREART = '" + nomArt + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private DataTable fillDataTable()
        {
            string query = "select * from vistaArticuloTallas where REFERENCIA = '" + getReferencia(comboArticulo.Text) + "'";
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(query, conexionSql.getConnection());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        private void fillGridView()
        {
            DataTable dt = fillDataTable();
            gridViewArticulo.Columns[0].Width = 90;
            gridViewArticulo.Columns[1].Width = 90;
            gridViewArticulo.Columns[2].Width = 90;
            gridViewArticulo.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                gridViewArticulo.Rows.Add(1);
                gridViewArticulo.Rows[i].Cells[0].Value = row.ItemArray[0].ToString();
                gridViewArticulo.Rows[i].Cells[1].Value = row.ItemArray[1].ToString();
                gridViewArticulo.Rows[i].Cells[2].Value = row.ItemArray[2].ToString();
                i++;
            }

            gridViewArticulo.Columns[0].ReadOnly = true;
            gridViewArticulo.Columns[1].ReadOnly = true;
            gridViewArticulo.Columns[2].ReadOnly = true;
            gridViewArticulo.CurrentCell = gridViewArticulo.Rows[0].Cells[3];
            gridViewArticulo.BeginEdit(true);
        }

        private void comboArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            do
            {
                foreach (DataGridViewRow row in gridViewArticulo.Rows)
                {
                    try
                    {
                        gridViewArticulo.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (gridViewArticulo.Rows.Count > 1);
            fillGridView();
            nombArt = comboArticulo.Text;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void gridViewArticulo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private int sumarCantidad()
        {
            int suma = 0;
            foreach (DataGridViewRow row in gridViewArticulo.Rows)
            {
                suma += Int32.Parse(row.Cells[3].Value.ToString());
            }
            return suma;
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
            foreach (DataGridViewRow row in this.gridViewArticulo.Rows)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarGuardar())
            {
                DataTable data1 = new DataTable();
                foreach (DataGridViewColumn col in gridViewArticulo.Columns)
                {
                    data1.Columns.Add(col.HeaderText);
                }

                foreach (DataGridViewRow row in gridViewArticulo.Rows)
                {
                    DataRow dRow = data1.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    data1.Rows.Add(dRow);
                }
                grids.Add(nombArt, data1);
                data.Rows.Add(1);
                data.Rows[data.RowCount - 1].Cells[0].Value = nombArt;
                data.Rows[data.RowCount - 1].Cells[1].Value = sumarCantidad();
                this.Close();
            }
            else
            {
                MessageBox.Show("El valor de salida no puede ser mayor que la cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
