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
    public partial class BusquedaFactura : Form
    {
        private string tipo;
        private ConexionSQL conexionSql;

        public BusquedaFactura()
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
            string query = "select * from vistaFactura";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewFactura.ReadOnly = true;
            gridViewFactura.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewFactura.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewFactura.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (tipo.Equals("Emitir"))
            {
                comboBusqueda.Items.RemoveAt(3);
                var bd = (BindingSource)gridViewFactura.DataSource;
                var dt = (DataTable)bd.DataSource;
                dt.DefaultView.RowFilter = string.Format(gridViewFactura.Columns[3].DataPropertyName + " like '%{0}%'", "No");
                gridViewFactura.Refresh();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (gridViewFactura.SelectedRows.Count >= 1)
            {
                int factura = Int32.Parse(gridViewFactura.SelectedRows[0].Cells[0].Value.ToString());
                if (tipo.Equals("Emitir"))
                {
                    EmitirFactura form = new EmitirFactura(tipo, factura);
                    form.Text = tipo + " Factura";
                    form.Show();
                }
                if (gridViewFactura.SelectedRows[0].Cells[3].Value.ToString().Equals("Si") && tipo.Equals("Consultar"))
                {
                    EmitirFactura form = new EmitirFactura("", factura);
                    form.Text = "Factura";
                    form.Show();
                }
                if (gridViewFactura.SelectedRows[0].Cells[3].Value.ToString().Equals("No") && !tipo.Equals("Emitir"))
                {
                    Factura form = new Factura(tipo, factura);
                    form.Text = tipo + " Factura";
                    form.Show();
                }
                btnSalir.PerformClick();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBusqueda.Clear();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridViewFactura_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnConsultar.PerformClick();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            var bd = (BindingSource)gridViewFactura.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format(gridViewFactura.Columns[comboBusqueda.SelectedIndex].DataPropertyName + " like '%{0}%'", txtBusqueda.Text.Trim().Replace("'", "''"));
            gridViewFactura.Refresh();
        }
    }
}
