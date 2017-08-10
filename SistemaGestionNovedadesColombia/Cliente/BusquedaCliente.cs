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
        private String tipo;
        private ConexionSQL conexionSql;

        public BusquedaCliente()
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            MaximizeBox = false;
            MinimizeBox = false;
            initGridView();
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }

        private void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from ClienteZona";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridViewCliente.ReadOnly = true;
            gridViewCliente.DataSource = ds.Tables[0];
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
            RegistroCliente form = new RegistroCliente(tipo);
            form.Text = tipo + " Cliente";
            form.Show();
            btnSalir.PerformClick();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
