using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cliente form = new Cliente();
            form.Text = "Registrar Cliente";
            form.Show();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }

        private void informaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IngresoSistema f = new IngresoSistema();
            f.Show();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        public void setStatusBar(String user)
        {
            dateStatusLabel.Text = "Fecha: " + DateTime.Now.ToShortDateString();
            activeUserStarusLabel.Text = "Usuario: " + user;
        }
    }
}
