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
                numericIVA.Value = reader.GetDecimal(1);
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

            cmd.Parameters.Add("@IVA", SqlDbType.Decimal).Value = numericIVA.Value;
            cmd.Parameters.Add("@CLIENTE", SqlDbType.VarChar).Value = txtClienteDefecto.Text;

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                guardarParams();
                MessageBox.Show("Parametros guardados con exito.", "Parametros", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            form.Text = "Modificar Grupo de Talla y Color";
            form.Show();
            this.Close();
        }
    }
}
