namespace SistemaGestionNovedadesColombia
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clienteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarProveedor = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarProveedor = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarProveedor = new System.Windows.Forms.ToolStripMenuItem();
            this.inventarioMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.articuloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.darDeAltaArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.darDeBajaArticulo = new System.Windows.Forms.ToolStripMenuItem();
            this.stockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entradaInventario = new System.Windows.Forms.ToolStripMenuItem();
            this.salidaInventario = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.emitirFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.vendedorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarVendedor = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarVendedor = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarVendedor = new System.Windows.Forms.ToolStripMenuItem();
            this.adminMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.sistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametrosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarGrupoDeTallaYColor = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaciónMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualUsuarioMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.activeUserStarusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clienteMenuItem,
            this.proveedorMenuItem,
            this.inventarioMenuItem,
            this.facturaMenuItem,
            this.vendedorMenuItem,
            this.adminMenuItem,
            this.usuarioMenuItem,
            this.ayudaMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1004, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clienteMenuItem
            // 
            this.clienteMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarCliente,
            this.consultarCliente,
            this.modificarCliente,
            this.eliminarCliente});
            this.clienteMenuItem.Name = "clienteMenuItem";
            this.clienteMenuItem.Size = new System.Drawing.Size(67, 24);
            this.clienteMenuItem.Text = "Cliente";
            // 
            // registrarCliente
            // 
            this.registrarCliente.Name = "registrarCliente";
            this.registrarCliente.Size = new System.Drawing.Size(148, 26);
            this.registrarCliente.Text = "Registrar";
            this.registrarCliente.Click += new System.EventHandler(this.registrarToolStripMenuItem_Click);
            // 
            // consultarCliente
            // 
            this.consultarCliente.Name = "consultarCliente";
            this.consultarCliente.Size = new System.Drawing.Size(148, 26);
            this.consultarCliente.Text = "Consultar";
            this.consultarCliente.Click += new System.EventHandler(this.consultarToolStripMenuItem_Click);
            // 
            // modificarCliente
            // 
            this.modificarCliente.Name = "modificarCliente";
            this.modificarCliente.Size = new System.Drawing.Size(148, 26);
            this.modificarCliente.Text = "Modificar";
            this.modificarCliente.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarCliente
            // 
            this.eliminarCliente.Name = "eliminarCliente";
            this.eliminarCliente.Size = new System.Drawing.Size(148, 26);
            this.eliminarCliente.Text = "Eliminar";
            this.eliminarCliente.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // proveedorMenuItem
            // 
            this.proveedorMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarProveedor,
            this.consultarProveedor,
            this.modificarProveedor});
            this.proveedorMenuItem.Name = "proveedorMenuItem";
            this.proveedorMenuItem.Size = new System.Drawing.Size(89, 24);
            this.proveedorMenuItem.Text = "Proveedor";
            // 
            // registrarProveedor
            // 
            this.registrarProveedor.Name = "registrarProveedor";
            this.registrarProveedor.Size = new System.Drawing.Size(148, 26);
            this.registrarProveedor.Text = "Registrar";
            this.registrarProveedor.Click += new System.EventHandler(this.registrarToolStripMenuItem1_Click);
            // 
            // consultarProveedor
            // 
            this.consultarProveedor.Name = "consultarProveedor";
            this.consultarProveedor.Size = new System.Drawing.Size(148, 26);
            this.consultarProveedor.Text = "Consultar";
            this.consultarProveedor.Click += new System.EventHandler(this.consultarToolStripMenuItem1_Click);
            // 
            // modificarProveedor
            // 
            this.modificarProveedor.Name = "modificarProveedor";
            this.modificarProveedor.Size = new System.Drawing.Size(148, 26);
            this.modificarProveedor.Text = "Modificar";
            this.modificarProveedor.Click += new System.EventHandler(this.modificarToolStripMenuItem3_Click);
            // 
            // inventarioMenuItem
            // 
            this.inventarioMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.articuloToolStripMenuItem,
            this.stockToolStripMenuItem});
            this.inventarioMenuItem.Name = "inventarioMenuItem";
            this.inventarioMenuItem.Size = new System.Drawing.Size(87, 24);
            this.inventarioMenuItem.Text = "Inventario";
            // 
            // articuloToolStripMenuItem
            // 
            this.articuloToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarArticulo,
            this.consultarArticulo,
            this.modificarArticulo,
            this.darDeAltaArticulo,
            this.darDeBajaArticulo});
            this.articuloToolStripMenuItem.Name = "articuloToolStripMenuItem";
            this.articuloToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.articuloToolStripMenuItem.Text = "Articulo";
            // 
            // registrarArticulo
            // 
            this.registrarArticulo.Name = "registrarArticulo";
            this.registrarArticulo.Size = new System.Drawing.Size(178, 26);
            this.registrarArticulo.Text = "Registrar";
            this.registrarArticulo.Click += new System.EventHandler(this.registrarToolStripMenuItem_Click_1);
            // 
            // consultarArticulo
            // 
            this.consultarArticulo.Name = "consultarArticulo";
            this.consultarArticulo.Size = new System.Drawing.Size(178, 26);
            this.consultarArticulo.Text = "Consultar";
            this.consultarArticulo.Click += new System.EventHandler(this.consultarToolStripMenuItem_Click_1);
            // 
            // modificarArticulo
            // 
            this.modificarArticulo.Name = "modificarArticulo";
            this.modificarArticulo.Size = new System.Drawing.Size(178, 26);
            this.modificarArticulo.Text = "Modificar";
            this.modificarArticulo.Click += new System.EventHandler(this.modificarToolStripMenuItem1_Click_1);
            // 
            // darDeAltaArticulo
            // 
            this.darDeAltaArticulo.Name = "darDeAltaArticulo";
            this.darDeAltaArticulo.Size = new System.Drawing.Size(178, 26);
            this.darDeAltaArticulo.Text = "Reincorporar";
            this.darDeAltaArticulo.Click += new System.EventHandler(this.darDeAltaArticulo_Click);
            // 
            // darDeBajaArticulo
            // 
            this.darDeBajaArticulo.Name = "darDeBajaArticulo";
            this.darDeBajaArticulo.Size = new System.Drawing.Size(178, 26);
            this.darDeBajaArticulo.Text = "Desincorporar";
            this.darDeBajaArticulo.Click += new System.EventHandler(this.darDeBajaArticulo_Click);
            // 
            // stockToolStripMenuItem
            // 
            this.stockToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entradaInventario,
            this.salidaInventario});
            this.stockToolStripMenuItem.Name = "stockToolStripMenuItem";
            this.stockToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.stockToolStripMenuItem.Text = "Stock";
            // 
            // entradaInventario
            // 
            this.entradaInventario.Name = "entradaInventario";
            this.entradaInventario.Size = new System.Drawing.Size(135, 26);
            this.entradaInventario.Text = "Entrada";
            this.entradaInventario.Click += new System.EventHandler(this.entradaToolStripMenuItem_Click);
            // 
            // salidaInventario
            // 
            this.salidaInventario.Name = "salidaInventario";
            this.salidaInventario.Size = new System.Drawing.Size(135, 26);
            this.salidaInventario.Text = "Salida";
            this.salidaInventario.Click += new System.EventHandler(this.salidaToolStripMenuItem_Click);
            // 
            // facturaMenuItem
            // 
            this.facturaMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarFactura,
            this.consultarFactura,
            this.emitirFactura});
            this.facturaMenuItem.Name = "facturaMenuItem";
            this.facturaMenuItem.Size = new System.Drawing.Size(96, 24);
            this.facturaMenuItem.Text = "Facturación";
            // 
            // registrarFactura
            // 
            this.registrarFactura.Name = "registrarFactura";
            this.registrarFactura.Size = new System.Drawing.Size(181, 26);
            this.registrarFactura.Text = "Registrar ";
            this.registrarFactura.Click += new System.EventHandler(this.registrarToolStripMenuItem2_Click);
            // 
            // consultarFactura
            // 
            this.consultarFactura.Name = "consultarFactura";
            this.consultarFactura.Size = new System.Drawing.Size(181, 26);
            this.consultarFactura.Text = "Consultar";
            this.consultarFactura.Click += new System.EventHandler(this.consultarPedidoToolStripMenuItem_Click);
            // 
            // emitirFactura
            // 
            this.emitirFactura.Name = "emitirFactura";
            this.emitirFactura.Size = new System.Drawing.Size(181, 26);
            this.emitirFactura.Text = "Emitir";
            this.emitirFactura.Click += new System.EventHandler(this.emitirFactura_Click);
            // 
            // vendedorMenuItem
            // 
            this.vendedorMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarVendedor,
            this.consultarVendedor,
            this.modificarVendedor});
            this.vendedorMenuItem.Name = "vendedorMenuItem";
            this.vendedorMenuItem.Size = new System.Drawing.Size(85, 24);
            this.vendedorMenuItem.Text = "Vendedor";
            // 
            // registrarVendedor
            // 
            this.registrarVendedor.Name = "registrarVendedor";
            this.registrarVendedor.Size = new System.Drawing.Size(148, 26);
            this.registrarVendedor.Text = "Registrar";
            this.registrarVendedor.Click += new System.EventHandler(this.registrarToolStripMenuItem3_Click);
            // 
            // consultarVendedor
            // 
            this.consultarVendedor.Name = "consultarVendedor";
            this.consultarVendedor.Size = new System.Drawing.Size(148, 26);
            this.consultarVendedor.Text = "Consultar";
            this.consultarVendedor.Click += new System.EventHandler(this.consultarToolStripMenuItem2_Click);
            // 
            // modificarVendedor
            // 
            this.modificarVendedor.Name = "modificarVendedor";
            this.modificarVendedor.Size = new System.Drawing.Size(148, 26);
            this.modificarVendedor.Text = "Modificar";
            this.modificarVendedor.Click += new System.EventHandler(this.modificarToolStripMenuItem1_Click);
            // 
            // adminMenuItem
            // 
            this.adminMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuarioToolStripMenuItem,
            this.sistemaToolStripMenuItem});
            this.adminMenuItem.Name = "adminMenuItem";
            this.adminMenuItem.Size = new System.Drawing.Size(121, 24);
            this.adminMenuItem.Text = "Administración";
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarUsuario,
            this.modificarUsuario});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // registrarUsuario
            // 
            this.registrarUsuario.Name = "registrarUsuario";
            this.registrarUsuario.Size = new System.Drawing.Size(148, 26);
            this.registrarUsuario.Text = "Registrar";
            this.registrarUsuario.Click += new System.EventHandler(this.registrarToolStripMenuItem4_Click);
            // 
            // modificarUsuario
            // 
            this.modificarUsuario.Name = "modificarUsuario";
            this.modificarUsuario.Size = new System.Drawing.Size(148, 26);
            this.modificarUsuario.Text = "Modificar";
            this.modificarUsuario.Click += new System.EventHandler(this.modificarToolStripMenuItem2_Click);
            // 
            // sistemaToolStripMenuItem
            // 
            this.sistemaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametrosMenuItem,
            this.registrarGrupoDeTallaYColor});
            this.sistemaToolStripMenuItem.Name = "sistemaToolStripMenuItem";
            this.sistemaToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.sistemaToolStripMenuItem.Text = "Sistema";
            // 
            // parametrosMenuItem
            // 
            this.parametrosMenuItem.Name = "parametrosMenuItem";
            this.parametrosMenuItem.Size = new System.Drawing.Size(294, 26);
            this.parametrosMenuItem.Text = "Parámetros";
            this.parametrosMenuItem.Click += new System.EventHandler(this.parametrosToolStripMenuItem_Click);
            // 
            // registrarGrupoDeTallaYColor
            // 
            this.registrarGrupoDeTallaYColor.Name = "registrarGrupoDeTallaYColor";
            this.registrarGrupoDeTallaYColor.Size = new System.Drawing.Size(294, 26);
            this.registrarGrupoDeTallaYColor.Text = "Registrar Grupo de Talla y Color";
            this.registrarGrupoDeTallaYColor.Click += new System.EventHandler(this.registrarGrupoDeTallaYColorToolStripMenuItem_Click);
            // 
            // usuarioMenuItem
            // 
            this.usuarioMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarToolStripMenuItem});
            this.usuarioMenuItem.Name = "usuarioMenuItem";
            this.usuarioMenuItem.Size = new System.Drawing.Size(71, 24);
            this.usuarioMenuItem.Text = "Usuario";
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click_1);
            // 
            // ayudaMenuItem
            // 
            this.ayudaMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informaciónMenuItem,
            this.manualUsuarioMenuItem});
            this.ayudaMenuItem.Name = "ayudaMenuItem";
            this.ayudaMenuItem.Size = new System.Drawing.Size(63, 24);
            this.ayudaMenuItem.Text = "Ayuda";
            // 
            // informaciónMenuItem
            // 
            this.informaciónMenuItem.Name = "informaciónMenuItem";
            this.informaciónMenuItem.Size = new System.Drawing.Size(187, 26);
            this.informaciónMenuItem.Text = "Información";
            this.informaciónMenuItem.Click += new System.EventHandler(this.informaciónToolStripMenuItem_Click);
            // 
            // manualUsuarioMenuItem
            // 
            this.manualUsuarioMenuItem.Name = "manualUsuarioMenuItem";
            this.manualUsuarioMenuItem.Size = new System.Drawing.Size(187, 26);
            this.manualUsuarioMenuItem.Text = "Manual Usuario";
            this.manualUsuarioMenuItem.Click += new System.EventHandler(this.manualUsuarioToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.activeUserStarusLabel,
            this.toolStripStatusLabel1,
            this.dateStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1004, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(238, 20);
            this.toolStripStatusLabel2.Text = "Empresa: Novedades de Colombia";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // activeUserStarusLabel
            // 
            this.activeUserStarusLabel.Name = "activeUserStarusLabel";
            this.activeUserStarusLabel.Size = new System.Drawing.Size(36, 20);
            this.activeUserStarusLabel.Text = "user";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // dateStatusLabel
            // 
            this.dateStatusLabel.Name = "dateStatusLabel";
            this.dateStatusLabel.Size = new System.Drawing.Size(39, 20);
            this.dateStatusLabel.Text = "date";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SistemaGestionNovedadesColombia.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1004, 586);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "Principal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clienteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturaMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendedorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarCliente;
        private System.Windows.Forms.ToolStripMenuItem modificarCliente;
        private System.Windows.Forms.ToolStripMenuItem consultarCliente;
        private System.Windows.Forms.ToolStripMenuItem eliminarCliente;
        private System.Windows.Forms.ToolStripMenuItem registrarProveedor;
        private System.Windows.Forms.ToolStripMenuItem consultarProveedor;
        private System.Windows.Forms.ToolStripMenuItem registrarFactura;
        private System.Windows.Forms.ToolStripMenuItem consultarFactura;
        private System.Windows.Forms.ToolStripMenuItem registrarVendedor;
        private System.Windows.Forms.ToolStripMenuItem modificarVendedor;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarUsuario;
        private System.Windows.Forms.ToolStripMenuItem modificarUsuario;
        private System.Windows.Forms.ToolStripMenuItem sistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaciónMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualUsuarioMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel activeUserStarusLabel;
        private System.Windows.Forms.ToolStripStatusLabel dateStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem modificarProveedor;
        private System.Windows.Forms.ToolStripMenuItem consultarVendedor;
        private System.Windows.Forms.ToolStripMenuItem parametrosMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventarioMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarGrupoDeTallaYColor;
        private System.Windows.Forms.ToolStripMenuItem usuarioMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articuloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarArticulo;
        private System.Windows.Forms.ToolStripMenuItem consultarArticulo;
        private System.Windows.Forms.ToolStripMenuItem modificarArticulo;
        private System.Windows.Forms.ToolStripMenuItem stockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entradaInventario;
        private System.Windows.Forms.ToolStripMenuItem salidaInventario;
        private System.Windows.Forms.ToolStripMenuItem darDeBajaArticulo;
        private System.Windows.Forms.ToolStripMenuItem emitirFactura;
        private System.Windows.Forms.ToolStripMenuItem darDeAltaArticulo;
    }
}

