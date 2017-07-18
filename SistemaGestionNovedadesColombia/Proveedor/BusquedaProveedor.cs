using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Proveedor
{
    public partial class BusquedaProveedor : Form
    {
        private String tipo;

        public BusquedaProveedor()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Proveedor form = new Proveedor(tipo);
            form.Text = tipo + " Cliente";
            form.Show();
            btnSalir.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnConsultar.PerformClick();
        }
    }
}
