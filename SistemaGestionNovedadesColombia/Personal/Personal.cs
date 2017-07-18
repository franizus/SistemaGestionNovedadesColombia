using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Personal
{
    public partial class Personal : Form
    {
        public Personal(String tipo)
        {
            InitializeComponent();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = txtID;
            initComponents(tipo);
        }

        private void initComponents(String tipo)
        {
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 45;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 45;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else if (tipo.Equals("Consultar"))
            {
                comboStatus.Enabled = false;
                txtID.Enabled = false;
                txtNombre.Enabled = false;
                textBox1.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown1.Enabled = false;
                tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[3].Height = 10;
                btnSalir1.Height = 31;
            }
            
            comboStatus.SelectedIndex = 0;
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vendedor guardado con exito.", "Registro Vendedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            comboStatus.SelectedIndex = 0;
            txtID.Text = "";
            txtNombre.Text = "";
            textBox1.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }
    }
}
