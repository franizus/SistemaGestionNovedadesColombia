﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestionNovedadesColombia.Inventario.Stock
{
    public partial class Entrada : Form
    {
        private Dictionary<string, DataTable> grids;
        private ConexionSQL conexionSql;

        public Entrada()
        {
            InitializeComponent();
            conexionSql = new ConexionSQL();
            this.CenterToScreen();
            MaximizeBox = false;
            MinimizeBox = false;
            grids = new Dictionary<string, DataTable>();
            fillComboProveedor();
            comboProveedor.SelectedIndex = 0;
            gridViewEntrada.Columns[0].Width = 150;
            gridViewEntrada.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void fillComboProveedor()
        {
            conexionSql.Conectar();
            string query = "select NOMBRE from PROVEEDOR";
            SqlCommand sqlCmd = new SqlCommand(query, conexionSql.getConnection());
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboProveedor.Items.Add(sqlReader["NOMBRE"].ToString());
            }

            sqlReader.Close();
            conexionSql.Desconectar();
        }

        private string getReferencia(string nomArt)
        {
            string refe = "";

            conexionSql.Conectar();
            SqlCommand cmd = new SqlCommand("select REFERENCIA from ARTICULO where NOMBREART = '" + nomArt + "'", conexionSql.getConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                refe = reader.GetString(0);
            }
            conexionSql.Desconectar();

            return refe;
        }

        private void guardarEntrada()
        {
            foreach (var grid in grids)
            {
                string referencia = getReferencia(grid.Key);
                foreach (DataRow row in grid.Value.Rows)
                {
                    conexionSql.Conectar();
                    SqlCommand cmd = new SqlCommand("registrarEntrada", conexionSql.getConnection());
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@REFERENCIA", SqlDbType.VarChar).Value = referencia;
                    cmd.Parameters.Add("@TALLA", SqlDbType.VarChar).Value = row.ItemArray[0].ToString();
                    cmd.Parameters.Add("@COLOR", SqlDbType.VarChar).Value = row.ItemArray[1].ToString();
                    cmd.Parameters.Add("@CANTIDAD", SqlDbType.Int).Value = Int32.Parse(row.ItemArray[2].ToString());

                    cmd.ExecuteNonQuery();
                    conexionSql.Desconectar();
                }
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            ArticuloStock ast = new ArticuloStock(comboProveedor.Text, grids, gridViewEntrada);
            ast.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            grids.Remove(gridViewEntrada.SelectedRows[0].Cells[0].Value.ToString());
            gridViewEntrada.Rows.RemoveAt(gridViewEntrada.SelectedRows[0].Index);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gridViewEntrada.RowCount >= 1)
            {
                guardarEntrada();
                MessageBox.Show("Entrada guardada con éxito.", "Entrada", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se ha ingresado ninguna entrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
