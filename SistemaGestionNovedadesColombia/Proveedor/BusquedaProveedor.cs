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

namespace SistemaGestionNovedadesColombia.Proveedor
{
    public partial class BusquedaProveedor : Form
    {
        private string tipo;
        private ConexionSQL conexionSql;

        public BusquedaProveedor()
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            MaximizeBox = false;
            MinimizeBox = false;
            initGridView();
            comboBusqueda.SelectedIndex = 0;
        }

        public void setTipo(string tipo)
        {
            this.tipo = tipo;
        }

        private void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from vistaProveedor";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewProveedor.ReadOnly = true;
            gridViewProveedor.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewProveedor.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (gridViewProveedor.SelectedRows.Count >= 1)
            {
                String idProveedor = gridViewProveedor.SelectedRows[0].Cells[0].Value.ToString();
                Proveedor form = new Proveedor(tipo, idProveedor);
                form.Text = tipo + " Proveedor";
                form.Show();
                btnSalir.PerformClick();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBusqueda.Clear();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            var bd = (BindingSource)gridViewProveedor.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewProveedor.Columns[comboBusqueda.SelectedIndex].DataPropertyName + " like '%{0}%'", txtBusqueda.Text.Trim().Replace("'", "''"));
            gridViewProveedor.Refresh();
        }
    }
}
