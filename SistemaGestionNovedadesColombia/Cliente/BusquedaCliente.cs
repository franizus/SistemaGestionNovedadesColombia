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
    public partial class BusquedaCliente : Form
    {
        private string tipo;
        private ConexionSQL conexionSql;

        public BusquedaCliente()
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
            string query = "select * from ClienteZona";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewCliente.ReadOnly = true;
            gridViewCliente.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewCliente.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (gridViewCliente.SelectedRows.Count >= 1)
            {
                string idCliente = gridViewCliente.SelectedRows[0].Cells[0].Value.ToString();
                Cliente form = new Cliente(tipo, idCliente);
                form.Text = tipo + " Cliente";
                form.Show();
                btnSalir.PerformClick();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBusqueda.Clear();
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            var bd = (BindingSource)gridViewCliente.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewCliente.Columns[comboBusqueda.SelectedIndex].DataPropertyName + " like '%{0}%'", txtBusqueda.Text.Trim().Replace("'", "''"));
            gridViewCliente.Refresh();
        }
    }
}
