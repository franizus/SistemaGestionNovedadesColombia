using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Administracion.Parametros
{
    public partial class Parametros : Form
    {
        private ConexionSQL conexionSql;

        public Parametros()
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            initGridView();
            fillForm();
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from PARAMETROS", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtClienteDefecto.Text = reader.GetString(2);
                numericIVA.Value = reader.GetDecimal(1) * 100;
            }
            conexionSql.Desconectar();
        }

        private void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from tallaColor";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewTallaColor.ReadOnly = true;
            gridViewTallaColor.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewTallaColor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridViewTallaColor.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtClienteDefecto.Text))
                err += "-->Cliente por defecto\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }

            return temp;
        }

        private void guardarParams()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("modifyParams", conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IVA", SqlDbType.Decimal).Value = numericIVA.Value / 100;
            cmd.Parameters.Add("@CLIENTE", SqlDbType.VarChar).Value = txtClienteDefecto.Text;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                guardarParams();
                MessageBox.Show("Parámetros guardados con éxito.", "Parámetros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void gridViewTallaColor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String nombre = gridViewTallaColor.SelectedRows[0].Cells[0].Value.ToString();
            GrupoTallaColor form = new GrupoTallaColor(nombre);
            form.Text = "Modificar Grupo de Tallas y Colores";
            form.Show();
            this.Close();
        }

        private void gridViewTallaColor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = gridViewTallaColor.HitTest(e.X, e.Y);
                gridViewTallaColor.ClearSelection();
                gridViewTallaColor.Rows[hti.RowIndex].Selected = true;
            }
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar?", "Eliminar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Int32 rowToDelete = gridViewTallaColor.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                String nombre = gridViewTallaColor.SelectedRows[0].Cells[0].Value.ToString();
                conexionSql.Conectar();
                SqlCommand command = new SqlCommand("DELETE FROM GRUPOTALLACOLOR WHERE NOMBREGTC = '" + nombre + "'", conexionSql.getConnection());
                command.ExecuteNonQuery();
                conexionSql.Desconectar();
                gridViewTallaColor.Rows.RemoveAt(rowToDelete);
                gridViewTallaColor.ClearSelection();
                MessageBox.Show("Eliminado con éxito.", "Grupo de Tallas y Colores", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
