using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaGestionNovedadesColombia.Facturacion;
using SistemaGestionNovedadesColombia.Inventario;
using SistemaGestionNovedadesColombia.Proveedor;

namespace SistemaGestionNovedadesColombia
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cliente form = new Cliente("Registrar");
            form.Text = "Registrar Cliente";
            form.Show();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaCliente bc = new BusquedaCliente();
            bc.setTipo("Consultar");
            bc.Show();
        }

        private void informaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaCliente bc = new BusquedaCliente();
            bc.setTipo("Eliminar");
            bc.Show();
        }

        public void setStatusBar(String user)
        {
            dateStatusLabel.Text = "Fecha: " + DateTime.Now.ToShortDateString();
            activeUserStarusLabel.Text = "Usuario: " + user;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaCliente bc = new BusquedaCliente();
            bc.setTipo("Modificar");
            bc.Show();
        }

        private void manualUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(Directory.GetParent(Directory.GetParent(path).FullName).FullName, @"Resources\userManual.chm");
            Help.ShowHelp(this, "file://" + path);
        }

        private void registrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Proveedor.Proveedor form = new Proveedor.Proveedor("Registrar");
            form.Text = "Registrar Proveedor";
            form.Show();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BusquedaProveedor bc = new BusquedaProveedor();
            bc.setTipo("Consultar");
            bc.Show();
        }

        private void modificarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BusquedaProveedor bc = new BusquedaProveedor();
            bc.setTipo("Modificar");
            bc.Show();
        }

        private void eliminarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            BusquedaProveedor bc = new BusquedaProveedor();
            bc.setTipo("Eliminar");
            bc.Show();
        }

        private void agregarArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Producto p = new Producto("Registrar");
            p.Text = "Agregar Articulo Inventario";
            p.Show();
        }

        private void consultarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BusquedaArticulo ba = new BusquedaArticulo();
            ba.setTipo("Consultar");
            ba.Show();
        }

        private void modificarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            BusquedaArticulo ba = new BusquedaArticulo();
            ba.setTipo("Modificar");
            ba.Show();
        }

        private void eliminarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BusquedaArticulo ba = new BusquedaArticulo();
            ba.setTipo("Eliminar");
            ba.Show();
        }

        private void registrarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Factura f = new Factura("Registrar");
            f.Text = "Registrar Factura";
            f.Show();
        }

        private void consultarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaFactura bf = new BusquedaFactura();
            bf.setTipo("Consultar");
            bf.Show();
        }

        private void actualizarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusquedaFactura bf = new BusquedaFactura();
            bf.setTipo("Modificar");
            bf.Show();
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BusquedaFactura bf = new BusquedaFactura();
            bf.setTipo("Eliminar");
            bf.Show();
        }
    }
}
