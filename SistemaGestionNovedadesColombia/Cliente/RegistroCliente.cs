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
    public partial class RegistroCliente : Form
    {
        public RegistroCliente(String tipo)
        {
            InitializeComponent();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = comboIDType;
            initComponents(tipo);
        }

        private void initComponents(String tipo)
        {
            txtID.MaxLength = 13;

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
            else
            {
                comboIDType.Enabled = false;
                comboStatus.Enabled = false;
                comboZona.Enabled = false;
                txtID.Enabled = false;
                txtNombre.Enabled = false;
                txtEmail.Enabled = false;
                txtDireccion.Enabled = false;
                txtTelf.Enabled = false;
                if (tipo.Equals("Consultar"))
                {
                    tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[3].Height = 10;
                    btnSalir1.Height = 31;
                }
                if (tipo.Equals("Eliminar"))
                {
                    tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[4].Height = 10;
                    btnSalir2.Height = 31;
                    btnEliminar.Height = 31;
                }
            }

            comboIDType.SelectedIndex = 0;
            comboIDType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStatus.SelectedIndex = 0;
            comboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboZona.SelectedIndex = 0;
            comboZona.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void validarRegistro()
        {
            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            comboIDType.SelectedIndex = 0;
            comboStatus.SelectedIndex = 0;
            comboZona.SelectedIndex = 0;
            txtID.Text = "";
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            txtTelf.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RegistroCliente guardado con exito.", "Registro RegistroCliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void btnSalir2_Click_1(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RegistroCliente eliminado con exito.", "Eliminacion RegistroCliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void comboIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboIDType.SelectedIndex == 0)
            {
                txtID.MaxLength = 13;
            }
            else if (comboIDType.SelectedIndex == 1)
            {
                txtID.MaxLength = 10;
            }
            else
            {
                txtID.MaxLength = 0;
            }
        }

        private void txtTelf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
