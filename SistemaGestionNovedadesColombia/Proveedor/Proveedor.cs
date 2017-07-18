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
    public partial class Proveedor : Form
    {
        public Proveedor(String tipo)
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
                comboSubjType.Enabled = false;
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
            comboSubjType.SelectedIndex = 0;
            comboSubjType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proveedor guardado con exito.", "Registro Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            comboIDType.SelectedIndex = 0;
            comboStatus.SelectedIndex = 0;
            comboSubjType.SelectedIndex = 0;
            txtID.Text = "";
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            txtTelf.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proveedor eliminado con exito.", "Eliminacion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir2_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }
    }
}
