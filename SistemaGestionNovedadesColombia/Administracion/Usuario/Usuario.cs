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
    public partial class Usuario : Form
    {
        public Usuario(String tipo)
        {
            InitializeComponent();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = textBox1;
            initComponents(tipo);
        }

        private void initComponents(String tipo)
        {
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 25;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 65;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else
            {
                comboStatus.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                if (tipo.Equals("Consultar"))
                {
                    tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[3].Height = 10;
                    btnSalir1.Height = 31;
                }
            }
            
            comboStatus.SelectedIndex = 0;
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboStatus.SelectedIndex = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario guardado con exito.", "Usuario RegistroCliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
