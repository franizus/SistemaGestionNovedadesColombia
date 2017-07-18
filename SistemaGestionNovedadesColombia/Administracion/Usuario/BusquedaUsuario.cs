using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Administracion.Usuario
{
    public partial class BusquedaUsuario : Form
    {
        private String tipo;

        public BusquedaUsuario()
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
            Usuario form = new Usuario(tipo);
            form.Text = tipo + " Usuario";
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
