using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Facturacion
{
    public partial class BuscarCliente : Form
    {
        public BuscarCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistroCliente c = new RegistroCliente("Registrar");
            c.Text = "Registrar RegistroCliente";
            c.Show();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RegistroCliente agregado con exito.", "Agregar RegistroCliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
