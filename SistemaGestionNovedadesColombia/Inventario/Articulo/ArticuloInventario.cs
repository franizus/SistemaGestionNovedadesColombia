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

namespace SistemaGestionNovedadesColombia.Inventario
{
    public partial class Producto : Form
    {
        private ConexionSQL conexionSql;
        private bool modificar;

        public Producto(string tipo, string idArticulo)
        {
            InitializeComponent();
            this.CenterToScreen();
            conexionSql = new ConexionSQL();
            MaximizeBox = false;
            MinimizeBox = false;
            this.ActiveControl = txtReferencia;
            initComponents(tipo, idArticulo);
        }

        private void initComponents(string tipo, string idArticulo)
        {
            fillComboLinea();
            fillComboGrupo();

            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 49;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].Height = 41;

            if (tipo.Equals("Registrar") || tipo.Equals("Modificar"))
            {
                if (!idArticulo.Equals("") && tipo.Equals("Modificar"))
                {
                    txtReferencia.Text = idArticulo;
                    modificar = true;
                    fillForm();
                    initGridView();
                    txtReferencia.Enabled = false;
                    comboGrupo.Enabled = false;
                    comboLinea.Enabled = false;
                }
                if (tipo.Equals("Registrar"))
                {
                    comboLinea.SelectedIndex = 0;
                    comboGrupo.SelectedIndex = 0;
                    tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[1].Height = 0;
                }

                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 10;
                btnSalir.Height = 31;
                btnLimpiar.Height = 31;
                btnGuardar.Height = 31;
            }
            else
            {
                if (!idArticulo.Equals(""))
                {
                    txtReferencia.Text = idArticulo;
                    fillForm();
                    initGridView();
                }

                comboLinea.Enabled = false;
                comboGrupo.Enabled = false;
                txtNombre.Enabled = false;
                txtReferencia.Enabled = false;
                numericPrecioCompra.Enabled = false;
                numericPrecioVenta.Enabled = false;
                if (tipo.Equals("Consultar"))
                {
                    tableLayoutPanel1.RowStyles[3].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[3].Height = 10;
                    btnSalir1.Height = 31;
                }
                if (tipo.Equals("Desincorporar") || tipo.Equals("Reincorporar"))
                {
                    btnDarDe.Width += 28;
                    if (tipo.Equals("Desincorporar"))
                    {
                        btnDarDe.Text = "Desincorporar";
                    }
                    else
                    {
                        btnDarDe.Text = "Reincorporar";
                    }
                    tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[4].Height = 10;
                    btnSalir2.Height = 31;
                    btnDarDe.Height = 31;
                }
            }
        }

        private void fillComboLinea()
        {
            conexionSql.Conectar();
            string query = "select NOMBRE from PROVEEDOR";
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboLinea.Items.Add(sqlReader["NOMBRE"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private void fillComboGrupo()
        {
            conexionSql.Conectar();
            string query = "select NOMBREGTC from GRUPOTALLACOLOR";
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboGrupo.Items.Add(sqlReader["NOMBREGTC"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private string getLinea(string idprov)
        {
            string linea = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select NOMBRE from PROVEEDOR where IDPROVEEDOR = '" + idprov + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                linea = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return linea;
        }

        private string getIDProv(string nombreProv)
        {
            string id = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select IDPROVEEDOR from PROVEEDOR where NOMBRE = '" + nombreProv + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return id;
        }

        private int getEstado(string referencia)
        {
            int estado = 0;

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select ESTADO from ARTICULO where REFERENCIA = '" + referencia + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                estado = Convert.ToInt32(reader.GetBoolean(0));
            }
            conexionSql.Desconectar();

            return estado;
        }

        private void initGridView()
        {
            conexionSql.Conectar();
            string query = "select * from vistaArticuloTallas where REFERENCIA = '" + txtReferencia.Text + "'";
            var dataAdapter = new SqlDataAdapter(query, conexionSql.getConnection());
            var ds = new DataTable();
            dataAdapter.Fill(ds);
            BindingSource bsSource = new BindingSource();
            bsSource.DataSource = ds;
            gridViewArticulo.ReadOnly = true;
            gridViewArticulo.DataSource = bsSource;
            conexionSql.Desconectar();

            gridViewArticulo.Columns[3].Visible = false;
            gridViewArticulo.Columns[0].Width = 150;
            gridViewArticulo.Columns[1].Width = 150;
            gridViewArticulo.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void fillForm()
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select * from ARTICULO where REFERENCIA = '" + txtReferencia.Text + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtNombre.Text = reader.GetString(3);
                comboLinea.SelectedItem = getLinea(reader.GetString(1));
                comboGrupo.SelectedItem = reader.GetString(2);
                numericPrecioCompra.Value = reader.GetDecimal(4);
                numericPrecioVenta.Value = reader.GetDecimal(5);
            }
            conexionSql.Desconectar();
        }

        private bool validarRegistro()
        {
            bool temp = true;

            var err = "Campo Vacio :\n";
            if (string.IsNullOrWhiteSpace(txtReferencia.Text))
                err += "-->Referencia\n";
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                err += "-->Nombre\n";

            if (!err.Equals("Campo Vacio :\n"))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temp = false;
            }
            
            return temp;
        }

        private void guardarArticulo(string procedure, bool darDeBaja)
        {
            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand(procedure, conexionSql.getConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = txtReferencia.Text;
            cmd.Parameters.Add("@IDPROVEEDOR", SqlDbType.VarChar).Value = getIDProv(comboLinea.Text);
            cmd.Parameters.Add("@NOMBREGTC", SqlDbType.VarChar).Value = comboGrupo.Text;
            cmd.Parameters.Add("@NOMBREART", SqlDbType.VarChar).Value = txtNombre.Text;
            cmd.Parameters.Add("@PRECIOCOMPRA", SqlDbType.Money).Value = numericPrecioCompra.Value;
            cmd.Parameters.Add("@PRECIOVENTA", SqlDbType.Money).Value = numericPrecioVenta.Value;
            if (darDeBaja)
            {
                if (getEstado(txtReferencia.Text) == 0)
                {
                    cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = 1;
                }
                else
                {
                    cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = 0;
                }
            }
            else
            {
                cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = 0;
            }

            cmd.ExecuteNonQuery();
            conexionSql.Desconectar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRegistro())
            {
                if (modificar)
                {
                    guardarArticulo("actualizarArticulo", false);
                    MessageBox.Show("Artículo modificado con éxito.", "Modificar Artículo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    conexionSql.Conectar();
                    SqlCommand cmd = new SqlCommand("select * from ARTICULO where REFERENCIA = '" + txtReferencia.Text + "'", conexionSql.getConnection());
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        guardarArticulo("registrarArticulo", false);
                        conexionSql.Desconectar();
                        MessageBox.Show("Artículo registrado con éxito.", "Registrar Artículo", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Artículo ya se encuentra registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLimpiar.PerformClick();
                        conexionSql.Desconectar();
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!modificar)
            {
                comboLinea.SelectedIndex = 0;
                comboGrupo.SelectedIndex = 0;
                txtReferencia.Text = "";
            }
            txtNombre.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cambiar\nel estado del artículo?", "Cambiar Estado", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                guardarArticulo("actualizarArticulo", true);
                this.Close();
            }
        }

        private void btnSalir2_Click(object sender, EventArgs e)
        {
            btnSalir.PerformClick();
        }
    }
}
