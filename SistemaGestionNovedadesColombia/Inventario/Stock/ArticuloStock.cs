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
    public partial class ArticuloStock : Form
    {
        private DataGridViewComboBoxCell tallaCb;
        private DataGridViewComboBoxCell colorCb;
        private ConexionSQL conexionSql;
        private Dictionary<string, DataTable> grids;
        private DataGridView data;
        private string nombArt;

        public ArticuloStock(string nomProv, Dictionary<string, DataTable> grid, DataGridView dat)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            grids = grid;
            data = dat;
            fillComboArticulo(nomProv);
            comboArticulo.SelectedIndex = 0;
            gridViewArticulo.Columns[0].Width = 80;
            gridViewArticulo.Columns[1].Width = 80;
            gridViewArticulo.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void fillComboTalla(string nomArticulo)
        {
            string query = "select TALLAS from GRUPOTALLACOLOR where NOMBREGTC = '" + getNombreGTC(nomArticulo) + "'";
            conexionSql.Conectar();
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            conexionSql.Conectar();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                string[] words = sqlReader["TALLAS"].ToString().Split(' ');
                foreach (string word in words)
                {
                    tallaCb.Items.Add(word);
                }
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private string getNombreGTC(string nomArticulo)
        {
            string gtc = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select NOMBREGTC from ARTICULO where NOMBREART = '" + nomArticulo + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                gtc = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return gtc;
        }

        private void fillComboColor(string nomArticulo)
        {
            string query = "select COLORES from GRUPOTALLACOLOR where NOMBREGTC = '" + getNombreGTC(nomArticulo) + "'";
            conexionSql.Conectar();
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                string[] words = sqlReader["COLORES"].ToString().Split(' ');
                foreach (string word in words)
                {
                    colorCb.Items.Add(word);
                }
                
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            gridViewArticulo.Rows.Add(1);
            int numRows = gridViewArticulo.RowCount - 1;
            tallaCb = new DataGridViewComboBoxCell();
            fillComboTalla(comboArticulo.Text);
            colorCb = new DataGridViewComboBoxCell();
            fillComboColor(comboArticulo.Text);
            
            gridViewArticulo.Rows[numRows].Cells[0] = tallaCb;
            gridViewArticulo.Rows[numRows].Cells[0].Value = tallaCb.Items[0];
            gridViewArticulo.Rows[numRows].Cells[1] = colorCb;
            gridViewArticulo.Rows[numRows].Cells[1].Value = colorCb.Items[0];

            gridViewArticulo.CurrentCell = gridViewArticulo.Rows[numRows].Cells[2];
            gridViewArticulo.BeginEdit(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            gridViewArticulo.Rows.RemoveAt(gridViewArticulo.SelectedRows[0].Index);
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
            nombArt = comboArticulo.Text;
        }

        private void gridViewArticulo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (gridViewArticulo.CurrentCell.ColumnIndex == 2)
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

        private int sumarCantidad()
        {
            int suma = 0;
            foreach (DataGridViewRow row in gridViewArticulo.Rows)
            {
                suma += Int32.Parse(row.Cells[2].Value.ToString());
            }
            return suma;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
