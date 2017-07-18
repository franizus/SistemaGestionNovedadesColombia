using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Administracion.Parametros
{
    public partial class Paramentros : Form
    {
        public Paramentros()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Parametros guardados con exito.", "Registro Parametros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GrupoTallaColor gtc = new GrupoTallaColor();
            gtc.Show();
        }
    }
}
