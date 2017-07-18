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
    public partial class IngresoSistema : Form
    {
        public IngresoSistema()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals("admin") && txtContrasenia.Text.Equals("admin"))
            {
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.setStatusBar(txtUsuario.Text);
                mw.Closed += (s, args) => this.Close();
                mw.Show();
            }
            else
            {
                MessageBox.Show("El usuario o contraseña son incorrectos.", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContrasenia_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
