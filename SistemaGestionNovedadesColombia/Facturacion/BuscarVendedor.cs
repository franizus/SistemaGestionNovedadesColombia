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
    public partial class BuscarVendedor : Form
    {
        private ConexionSQL conexionSql;
        private TextBox vendedor;

        public BuscarVendedor(TextBox vend)
        {
            InitializeComponent();
            vendedor = vend;
            conexionSql = new ConexionSQL();
            MaximizeBox = false;
            MinimizeBox = false;
            initGridView();
            comboBusqueda.SelectedIndex = 0;
        }

        private void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from vistaVendedor";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewVendedor.ReadOnly = true;
            gridViewVendedor.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewVendedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewVendedor.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var bd = (BindingSource)gridViewVendedor.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewVendedor.Columns[2].DataPropertyName + " not like '%{0}%'", "Ina");
            gridViewVendedor.Refresh();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (gridViewVendedor.SelectedRows.Count >= 1)
            {
                vendedor.Text = gridViewVendedor.SelectedRows[0].Cells[1].Value.ToString();
                btnSalir.PerformClick();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un Vendedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBusqueda.Clear();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridViewVendedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSeleccionar.PerformClick();
        }
    }
}
