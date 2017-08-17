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

namespace SistemaGestionNovedadesColombia.Inventario
{
    public partial class BusquedaArticulo : Form
    {
        private string tipo;
        private ConexionSQL conexionSql;

        public BusquedaArticulo()
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            MaximizeBox = false;
            MinimizeBox = false;
            comboBusqueda.SelectedIndex = 0;
        }

        public void setTipo(string tipo)
        {
            this.tipo = tipo;
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
            if (tipo.Equals("Desincorporar") || tipo.Equals("Reincorporar"))
            {
                comboBusqueda.Items.RemoveAt(2);
                var bd = (BindingSource)gridViewArticulo.DataSource;
                var dt = (DataTable)bd.DataSource;
                if (tipo.Equals("Desincorporar"))
                {
                    dt.DefaultView.RowFilter = string.Format(gridViewArticulo.Columns[2].DataPropertyName + " like '%{0}%'", "Activo");
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Format(gridViewArticulo.Columns[2].DataPropertyName + " like '%{0}%'", "Ina");
                }
                gridViewArticulo.Refresh();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (gridViewArticulo.SelectedRows.Count >= 1)
            {
                string referencia = gridViewArticulo.SelectedRows[0].Cells[0].Value.ToString();
                Producto form = new Producto(tipo, referencia);
                form.Text = tipo + " Artículo";
                form.Show();
                btnSalir.PerformClick();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un artículo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var bd = (BindingSource)gridViewArticulo.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewArticulo.Columns[comboBusqueda.SelectedIndex].DataPropertyName + " like '%{0}%'", txtBusqueda.Text.Trim().Replace("'", "''"));
            gridViewArticulo.Refresh();
        }
    }
}
