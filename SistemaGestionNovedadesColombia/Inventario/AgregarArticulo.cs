using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Inventario
{
    public partial class AgregarArticulo : Form
    {
        public AgregarArticulo()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            TableLayoutPanel table = new TableLayoutPanel();
            table.ColumnCount = 3;
            table.RowCount = 1;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            ComboBox combo = new ComboBox();
            combo.Items.Add("S");
            combo.Items.Add("M");
            combo.Items.Add("L");
            combo.Items.Add("XL");
            combo.Dock = DockStyle.Fill;
            table.Controls.Add(combo);
            ComboBox comboColor = new ComboBox();
            comboColor.Items.Add("Azul");
            comboColor.Items.Add("Negro");
            comboColor.Items.Add("Beige");
            comboColor.Dock = DockStyle.Fill;
            table.Controls.Add(comboColor);
            TextBox text = new TextBox();
            text.Dock = DockStyle.Fill;
            table.Controls.Add(text);
            table.Width = flowLayoutPanel1.Width;
            table.Height = 30;
            flowLayoutPanel1.Controls.Add(table);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tallas y Colores agregados con exito.", "Agregar Articulo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
