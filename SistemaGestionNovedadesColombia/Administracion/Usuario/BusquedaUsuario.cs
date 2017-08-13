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

namespace SistemaGestionNovedadesColombia.Administracion.Usuario
{
    public partial class BusquedaUsuario : Form
    {
        private string usuario;
        private ConexionSQL conexionSql;

        public BusquedaUsuario(string user)
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            MaximizeBox = false;
            MinimizeBox = false;
            usuario = user;
            initGridView();
            comboBusqueda.SelectedIndex = 0;
        }

        private void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from vistaUsuario";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewUsuario.ReadOnly = true;
            gridViewUsuario.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewUsuario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewUsuario.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (gridViewUsuario.SelectedRows.Count >= 1)
            {
                String usuario = gridViewUsuario.SelectedRows[0].Cells[0].Value.ToString();
                Usuario form = new Usuario("Modificar", usuario);
                form.Text = "Modificar Usuario";
                form.Show();
                btnSalir.PerformClick();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var bd = (BindingSource)gridViewUsuario.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewUsuario.Columns[comboBusqueda.SelectedIndex].DataPropertyName + " like '%{0}%'", txtBusqueda.Text.Trim().Replace("'", "''"));
            gridViewUsuario.Refresh();
        }
    }
}
