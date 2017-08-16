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
    public partial class Factura : Form
    {
        public Factura(String tipo)
        {
            InitializeComponent();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = button1;
            initComponents(tipo);
        }

        private void initComponents(String tipo)
        {
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 33.33F;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 33.33F;
            tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[2].Height = 23.33F;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[3].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else
            {
                txtID.Enabled = false;
                textBox1.Enabled = false;
                richTextBox1.Enabled = false;
                dataGridView1.Enabled = false;
                if (tipo.Equals("Consultar"))
                {
                    tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[4].Height = 10;
                    btnSalir1.Height = 31;
                }
                if (tipo.Equals("Eliminar"))
                {
                    tableLayoutPanel1.RowStyles[5].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[5].Height = 10;
                    btnSalir2.Height = 31;
                    btnEliminar.Height = 31;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarCliente bc = new BuscarCliente();
            bc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BuscarVendedor bv = new BuscarVendedor();
            bv.Show();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Factura guardada con exito.\n¿Desea Imprimir?", "Impresion Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            btnSalir.PerformClick();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            textBox1.Text = "";
            richTextBox1.Text = "";
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
            MessageBox.Show("Factura eliminada con exito.", "Eliminacion Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir2_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BuscarArticulo ba = new BuscarArticulo();
            ba.Show();
        }
    }
}
