/*
CREAR BASE DE DATOS
*/
use master
drop database SGIV

create database SGIV
use SGIV




/*
CREAR TABLAS
*/
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLIENTE') and o.name = 'FK_CLIENTE_RELATIONS_ZONA')
alter table CLIENTE
   drop constraint FK_CLIENTE_RELATIONS_ZONA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ARTICULO') and o.name = 'FK_ARTICULO_RELATIONS_GRUPOTAL')
alter table ARTICULO
   drop constraint FK_ARTICULO_RELATIONS_GRUPOTAL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLIENTE') and o.name = 'FK_CLIENTE_RELATIONS_ZONA')
alter table CLIENTE
   drop constraint FK_CLIENTE_RELATIONS_ZONA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('TYCXPRODUCTO') and o.name = 'FK_TYCXPROD_RELATIONS_ARTICULO')
alter table TYCXPRODUCTO
   drop constraint FK_TYCXPROD_RELATIONS_ARTICULO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ARTICULO')
            and   name  = 'RELATIONSHIP_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index ARTICULO.RELATIONSHIP_4_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ARTICULO')
            and   name  = 'RELATIONSHIP_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index ARTICULO.RELATIONSHIP_2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CLIENTE')
            and   name  = 'RELATIONSHIP_1_FK'
            and   indid > 0
            and   indid < 255)
   drop index CLIENTE.RELATIONSHIP_1_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GRUPOTALLACOLOR')
            and   type = 'U')
   drop table GRUPOTALLACOLOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PARAMETROS')
            and   type = 'U')
   drop table PARAMETROS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PROVEEDOR')
            and   type = 'U')
   drop table PROVEEDOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VENDEDOR')
            and   type = 'U')
   drop table VENDEDOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENTE')
            and   type = 'U')
   drop table CLIENTE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TYCXPRODUCTO')
            and   name  = 'RELATIONSHIP_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index TYCXPRODUCTO.RELATIONSHIP_3_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TYCXPRODUCTO')
            and   type = 'U')
   drop table TYCXPRODUCTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO')
            and   type = 'U')
   drop table USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ZONA')
            and   type = 'U')
   drop table ZONA
go

/*==============================================================*/
/* Table: ARTICULO                                              */
/*==============================================================*/
create table ARTICULO (
   REFERENCIA           varchar(15)          not null,
   IDPROVEEDOR          varchar(15)          not null,
   NOMBREGTC            varchar(20)          not null,
   NOMBREART            varchar(100)         not null,
   PRECIOCOMPRA         money                not null,
   PRECIOVENTA          money                not null,
   constraint PK_ARTICULO primary key (REFERENCIA)
)
go
ALTER TABLE ARTICULO ADD ESTADO bit NOT NULL
GO

/*==============================================================*/
/* Index: RELATIONSHIP_2_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_2_FK on ARTICULO (IDPROVEEDOR ASC)
go

/*==============================================================*/
/* Index: RELATIONSHIP_4_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_4_FK on ARTICULO (NOMBREGTC ASC)
go


/*==============================================================*/
/* Table: CLIENTE                                               */
/*==============================================================*/
create table CLIENTE (
   IDCLIENTE            varchar(15)          not null,
   IDZONA               int                  not null,
   TIPOID               varchar(10)          not null,
   NOMBRE               varchar(50)          not null,
   CONTACTO             varchar(50)          null,
   TELEFONO             varchar(10)          null,
   EMAIL                varchar(30)          null,
   DIRECCION            varchar(100)         not null,
   constraint PK_CLIENTE primary key (IDCLIENTE)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_1_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_1_FK on CLIENTE (IDZONA ASC)
go

/*==============================================================*/
/* Table: DETALLEFACTURA                                        */
/*==============================================================*/
create table DETALLEFACTURA (
   IDFACTURA            int                  null,
   REFERENCIA           varchar(15)          not null,
   CANTIDAD             int                  not null,
   REFERENCIAF          varchar(15)          not null,
   DESCRIPCION          varchar(100)         not null,
   PRECIOUNIT           money                not null,
   PRECIOTOT            money                not null
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_5_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_5_FK on DETALLEFACTURA (IDFACTURA ASC)
go

/*==============================================================*/
/* Index: RELATIONSHIP_9_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_9_FK on DETALLEFACTURA (REFERENCIA ASC)
go

/*==============================================================*/
/* Table: FACTURA                                               */
/*==============================================================*/
create table FACTURA (
   IDFACTURA            int                  identity,
   IDVENDEDOR           varchar(15)          not null,
   IDCLIENTE            varchar(15)          null,
   FECHA                datetime             not null,
   TIPO                 bit                  not null,
   EMITIDO              bit                  not null,
   NUMEROFACT           varchar(20)          null,
   constraint PK_FACTURA primary key (IDFACTURA)
)
go
ALTER TABLE FACTURA ADD OBSERVACION varchar(200) null
GO
ALTER TABLE FACTURA ADD PRECIOTOTAL money not null
GO
ALTER TABLE FACTURA ADD SUBTOTAL money not null
GO
ALTER TABLE FACTURA ADD IVA decimal(3,2) not null
GO

/*==============================================================*/
/* Index: RELATIONSHIP_9_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_9_FK on FACTURA (IDCLIENTE ASC)
go

/*==============================================================*/
/* Index: RELATIONSHIP_10_FK                                    */
/*==============================================================*/

create nonclustered index RELATIONSHIP_10_FK on FACTURA (IDVENDEDOR ASC)
go

/*==============================================================*/
/* Table: GRUPOTALLACOLOR                                       */
/*==============================================================*/
create table GRUPOTALLACOLOR (
   NOMBREGTC            varchar(20)          not null,
   TALLAS               varchar(200)         not null,
   COLORES              varchar(200)         not null,
   constraint PK_GRUPOTALLACOLOR primary key (NOMBREGTC)
)
go

/*==============================================================*/
/* Table: PARAMETROS                                            */
/*==============================================================*/
create table PARAMETROS (
   PARAMID              int                  not null,
   IVA                  decimal(3,2)         not null,
   CLIENTE              varchar(20)          not null,
   constraint PK_PARAMETROS primary key nonclustered (PARAMID)
)
go

/*==============================================================*/
/* Table: PROVEEDOR                                             */
/*==============================================================*/
create table PROVEEDOR (
   IDPROVEEDOR          varchar(15)          not null,
   TIPOID               varchar(10)          not null,
   NOMBRE               varchar(50)          not null,
   CONTACTO             varchar(50)          null,
   TELEFONO             varchar(10)          null,
   EMAIL                varchar(30)          null,
   DIRECCION            varchar(100)         not null,
   constraint PK_PROVEEDOR primary key (IDPROVEEDOR)
)
go
ALTER TABLE PROVEEDOR ADD ESTADO bit NOT NULL
GO

/*==============================================================*/
/* Table: TYCXPRODUCTO                                          */
/*==============================================================*/
create table TYCXPRODUCTO (
   REFERENCIA           varchar(15)          not null,
   TALLA                varchar(10)          not null,
   COLOR                varchar(20)          not null,
   CANTIDAD             int                  not null
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_3_FK                                     */
/*==============================================================*/

create nonclustered index RELATIONSHIP_3_FK on TYCXPRODUCTO (REFERENCIA ASC)
go

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   USUARIO              varchar(15)          not null,
   CONTRASENIA          binary(64)           not null,
   NOMBRE               varchar(50)          not null,
   CLIENTEL             bit                  not null,
   CLIENTEE             bit                  not null,
   PROVEEDORL           bit                  not null,
   PROVEEDORE           bit                  not null,
   INVENTARIOL          bit                  not null,
   INVENTARIOE          bit                  not null,
   FACTURAL             bit                  not null,
   FACTURAE             bit                  not null,
   VENDEDORL            bit                  not null,
   VENDEDORE            bit                  not null,
   ADMINISTRACIONL      bit                  not null,
   ADMINISTRACIONE      bit                  not null,
   constraint PK_USUARIO primary key (USUARIO)
)
go
ALTER TABLE USUARIO ADD Salt UNIQUEIDENTIFIER 
GO
ALTER TABLE USUARIO ADD ESTADO bit NOT NULL
GO

/*==============================================================*/
/* Table: VENDEDOR                                              */
/*==============================================================*/
create table VENDEDOR (
   IDVENDEDOR           varchar(15)          not null,
   NOMBRE               varchar(50)          not null,
   TELEFONO             varchar(10)          null,
   DIRECCION            varchar(100)         null,
   ESTADO               bit                  not null,
   N1DESDE              money                not null,
   N1HASTA              money                not null,
   N1PORCENTAJE         decimal(3,2)         not null,
   N2DESDE              money                not null,
   N2HASTA              money                not null,
   N2PORCENTAJE         decimal(3,2)         not null,
   N3DESDE              money                not null,
   N3HASTA              money                not null,
   N3PORCENTAJE         decimal(3,2)         not null,
   N4DESDE              money                not null,
   N4HASTA              money                not null,
   N4PORCENTAJE         decimal(3,2)         not null,
   N5DESDE              money                not null,
   N5HASTA              money                not null,
   N5PORCENTAJE         decimal(3,2)         not null,
   constraint PK_VENDEDOR primary key (IDVENDEDOR)
)
go

/*==============================================================*/
/* Table: ZONA                                                  */
/*==============================================================*/
create table ZONA (
   IDZONA               int                  identity,
   CIUDAD               varchar(50)          not null,
   PROVINCIA            varchar(40)          not null,
   constraint PK_ZONA primary key (IDZONA)
)
go

alter table CLIENTE
   add constraint FK_CLIENTE_RELATIONS_ZONA foreign key (IDZONA)
      references ZONA (IDZONA)
go



/*
INGRESO DATOS
*/
insert ZONA values('Cuenca','Azuay')
insert ZONA values('Gualaceo','Azuay')
insert ZONA values('Paute','Azuay')
insert ZONA values('Santa Isabel','Azuay')
insert ZONA values('Chordeleg','Azuay')
insert ZONA values('Girón','Azuay')
insert ZONA values('Sígsig','Azuay')
insert ZONA values('San Fernando','Azuay')
insert ZONA values('Nabón','Azuay')
insert ZONA values('Guachapala','Azuay')
insert ZONA values('Pucará','Azuay')
insert ZONA values('San Felipe de Oña','Azuay')
insert ZONA values('Sevilla de Oro','Azuay')
insert ZONA values('El Pan','Azuay')
insert ZONA values('Camilo Ponce Enríquez','Azuay')
insert ZONA values('Guaranda','Bolívar')
insert ZONA values('San Miguel','Bolívar')
insert ZONA values('Caluma','Bolívar')
insert ZONA values('Echeandía','Bolívar')
insert ZONA values('San José de Chimbo','Bolívar')
insert ZONA values('Chillanes','Bolívar')
insert ZONA values('Las Naves','Bolívar')
insert ZONA values('La Troncal','Cañar')
insert ZONA values('Azogues','Cañar')
insert ZONA values('Cañar','Cañar')
insert ZONA values('Biblián','Cañar')
insert ZONA values('El Tambo','Cañar')
insert ZONA values('Suscal','Cañar')
insert ZONA values('Déleg','Cañar')
insert ZONA values('Tulcán','Carchi')
insert ZONA values('San Gabriel','Carchi')
insert ZONA values('El Ángel','Carchi')
insert ZONA values('Huaca','Carchi')
insert ZONA values('Mira','Carchi')
insert ZONA values('Bolívar','Carchi')
insert ZONA values('Riobamba','Chimborazo')
insert ZONA values('Cumandá','Chimborazo')
insert ZONA values('Guano','Chimborazo')
insert ZONA values('Alausír','Chimborazo')
insert ZONA values('Chambo','Chimborazo')
insert ZONA values('Chunchi','Chimborazo')
insert ZONA values('Pallatanga','Chimborazo')
insert ZONA values('Guamote','Chimborazo')
insert ZONA values('Villa La Unión (Cajabamba)','Chimborazo')
insert ZONA values('Penipe','Chimborazo')
insert ZONA values('Latacunga','Cotopaxi')
insert ZONA values('La Maná','Cotopaxi')
insert ZONA values('Salcedo','Cotopaxi')
insert ZONA values('Pujilí','Cotopaxi')
insert ZONA values('Saquisilí','Cotopaxi')
insert ZONA values('Sigchos','Cotopaxi')
insert ZONA values('El Corazón','Cotopaxi')
insert ZONA values('Machala','El Oro')
insert ZONA values('Pasaje','El Oro')
insert ZONA values('Santa Rosa','El Oro')
insert ZONA values('Huaquillas','El Oro')
insert ZONA values('El Guabo','El Oro')
insert ZONA values('Arenillas','El Oro')
insert ZONA values('Piñas','El Oro')
insert ZONA values('Zaruma','El Oro')
insert ZONA values('Portovelo','El Oro')
insert ZONA values('Balsas','El Oro')
insert ZONA values('Marcabelí','El Oro')
insert ZONA values('Paccha','El Oro')
insert ZONA values('La Victoria','El Oro')
insert ZONA values('Chilla','El Oro')
insert ZONA values('Esmeraldas','Esmeraldas')
insert ZONA values('Quinindé','Esmeraldas')
insert ZONA values('San Lorenzo','Esmeraldas')
insert ZONA values('Atacames','Esmeraldas')
insert ZONA values('Muisne','Esmeraldas')
insert ZONA values('Valdez (Limones)','Esmeraldas')
insert ZONA values('Rioverde','Esmeraldas')
insert ZONA values('Puerto Ayora','Galápagos')
insert ZONA values('Puerto Baquerizo Moreno','Galápagos')
insert ZONA values('Puerto Villamil','Galápagos')
insert ZONA values('Guayaquil','Guayas')
insert ZONA values('Durán','Guayas')
insert ZONA values('Milagro','Guayas')
insert ZONA values('Daule','Guayas')
insert ZONA values('Samborondón','Guayas')
insert ZONA values('El Empalme','Guayas')
insert ZONA values('El Triunfo','Guayas')
insert ZONA values('General Villamil','Guayas')
insert ZONA values('Balzar','Guayas')
insert ZONA values('Naranjito','Guayas')
insert ZONA values('Naranjal','Guayas')
insert ZONA values('Pedro Carbo','Guayas')
insert ZONA values('Yaguachi','Guayas')
insert ZONA values('Lomas de Sargentillo','Guayas')
insert ZONA values('Salitre','Guayas')
insert ZONA values('Balao','Guayas')
insert ZONA values('Santa Lucía','Guayas')
insert ZONA values('Palestina','Guayas')
insert ZONA values('Jujan','Guayas')
insert ZONA values('Nobol','Guayas')
insert ZONA values('Simón Bolívar','Guayas')
insert ZONA values('Coronel Marcelino','Guayas')
insert ZONA values('Colimes','Guayas')
insert ZONA values('Bucay','Guayas')
insert ZONA values('Isidro Ayora','Guayas')
insert ZONA values('Ibarra','Imbabura')
insert ZONA values('Otavalo','Imbabura')
insert ZONA values('Atuntaqui','Imbabura')
insert ZONA values('Cotacachi','Imbabura')
insert ZONA values('Pimampiro','Imbabura')
insert ZONA values('Urcuquí','Imbabura')
insert ZONA values('Loja','Loja')
insert ZONA values('Catamayo','Loja')
insert ZONA values('Cariamanga','Loja')
insert ZONA values('Macará','Loja')
insert ZONA values('Catacocha','Loja')
insert ZONA values('Alamor','Loja')
insert ZONA values('Celica','Loja')
insert ZONA values('Saraguro','Loja')
insert ZONA values('Zapotillo','Loja')
insert ZONA values('Pindal','Loja')
insert ZONA values('Amaluza','Loja')
insert ZONA values('Gonzanamá','Loja')
insert ZONA values('Chaguarpamba','Loja')
insert ZONA values('Quilanga','Loja')
insert ZONA values('Sozoranga','Loja')
insert ZONA values('Olmedo','Loja')
insert ZONA values('Quevedo','Los Ríos')
insert ZONA values('Babahoyo','Los Ríos')
insert ZONA values('Buena Fe','Los Ríos')
insert ZONA values('Ventanas','Los Ríos')
insert ZONA values('Vinces','Los Ríos')
insert ZONA values('Valencia','Los Ríos')
insert ZONA values('Montalvo','Los Ríos')
insert ZONA values('Mocache','Los Ríos')
insert ZONA values('Puebloviejo','Los Ríos')
insert ZONA values('Palenque','Los Ríos')
insert ZONA values('Catarama','Los Ríos')
insert ZONA values('Baba','Los Ríos')
insert ZONA values('Quinsaloma','Los Ríos')
insert ZONA values('Portoviejo','Manabí')
insert ZONA values('Manta','Manabí')
insert ZONA values('Chone','Manabí')
insert ZONA values('El Carmen','Manabí')
insert ZONA values('Montecristi','Manabí')
insert ZONA values('Jipijapa','Manabí')
insert ZONA values('Pedernales','Manabí')
insert ZONA values('Bahía de Caráquez','Manabí')
insert ZONA values('Calceta','Manabí')
insert ZONA values('Jaramijó','Manabí')
insert ZONA values('Tosagua','Manabí')
insert ZONA values('Puerto López','Manabí')
insert ZONA values('San Vicente','Manabí')
insert ZONA values('Santa Ana de Vuelta Larga','Manabí')
insert ZONA values('Rocafuerte','Manabí')
insert ZONA values('Paján','Manabí')
insert ZONA values('Flavio Alfaro','Manabí')
insert ZONA values('Jama','Manabí')
insert ZONA values('Junín','Manabí')
insert ZONA values('Pichincha','Manabí')
insert ZONA values('Sucre','Manabí')
insert ZONA values('Olmedo','Manabí')
insert ZONA values('Macas','Morona Santiago')
insert ZONA values('Sucúa','Morona Santiago')
insert ZONA values('Gualaquiza','Morona Santiago')
insert ZONA values('General Leonidas Plaza','Morona Santiago')
insert ZONA values('Palora','Morona Santiago')
insert ZONA values('Santiago de Méndez','Morona Santiago')
insert ZONA values('Logroño','Morona Santiago')
insert ZONA values('San Juan Bosco','Morona Santiago')
insert ZONA values('Santiago','Morona Santiago')
insert ZONA values('Taisha','Morona Santiago')
insert ZONA values('Huamboya','Morona Santiago')
insert ZONA values('Pablo Sexto','Morona Santiago')
insert ZONA values('Tena','Napo')
insert ZONA values('Archidona','Napo')
insert ZONA values('El Chaco','Napo')
insert ZONA values('Baeza','Napo')
insert ZONA values('Carlos Julio Arosemena','Napo')
insert ZONA values('Francisco de Orellana (Coca)','Orellana')
insert ZONA values('La Joya de los Sachas','Orellana')
insert ZONA values('Loreto','Orellana')
insert ZONA values('Tiputini','Orellana')
insert ZONA values('Puyo','Pastaza')
insert ZONA values('Santa Clara','Pastaza')
insert ZONA values('Arajuno','Pastaza')
insert ZONA values('Mera','Pastaza')
insert ZONA values('Quito','Pichincha')
insert ZONA values('Sangolquí','Pichincha')
insert ZONA values('Cayambe','Pichincha')
insert ZONA values('Machachi','Pichincha')
insert ZONA values('Tabacundo','Pichincha')
insert ZONA values('Pedro Vicente Maldonado','Pichincha')
insert ZONA values('San Miguel de Los Bancos','Pichincha')
insert ZONA values('Puerto Quito','Pichincha')
insert ZONA values('La Libertad','Santa Elena')
insert ZONA values('Santa Elena','Santa Elena')
insert ZONA values('Salinas','Santa Elena')
insert ZONA values('Santo Domingo','Santo Domingo de los Tsachilas')
insert ZONA values('La Concordia','Santo Domingo de los Tsachilas')
insert ZONA values('Nueva Loja','Sucumbíos')
insert ZONA values('Shushufindi','Sucumbíos')
insert ZONA values('Puerto El Carmen de Putumayo','Sucumbíos')
insert ZONA values('El Dorado de Cascales','Sucumbíos')
insert ZONA values('Lumbaqui','Sucumbíos')
insert ZONA values('Tarapoa','Sucumbíos')
insert ZONA values('La Bonita','Sucumbíos')
insert ZONA values('Ambato','Tungurahua')
insert ZONA values('Baños de Agua Santa','Tungurahua')
insert ZONA values('Pelileo','Tungurahua')
insert ZONA values('Píllaro','Tungurahua')
insert ZONA values('Quero','Tungurahua')
insert ZONA values('Cevallos','Tungurahua')
insert ZONA values('Patate','Tungurahua')
insert ZONA values('Tisaleo','Tungurahua')
insert ZONA values('Mocha','Tungurahua')
insert ZONA values('Zamora','Zamora Chinchipe')
insert ZONA values('Yantzaza','Zamora Chinchipe')
insert ZONA values('Zumba','Zamora Chinchipe')
insert ZONA values('El Pangui','Zamora Chinchipe')
insert ZONA values('Zumbi','Zamora Chinchipe')
insert ZONA values('Palanda','Zamora Chinchipe')
insert ZONA values('Guayzimi','Zamora Chinchipe')
insert ZONA values('28 de Mayo','Zamora Chinchipe')
insert ZONA values('Paquisha','Zamora Chinchipe')
go

insert into PARAMETROS(PARAMID, IVA, CLIENTE)
VALUES (1, 0.12, 'CONSUMIDOR FINAL')
GO

insert into GRUPOTALLACOLOR(NOMBREGTC, TALLAS, COLORES)
VALUES ('Brasier', '030 032 034 036 038', 'Blanco Beige Negro')
insert into GRUPOTALLACOLOR(NOMBREGTC, TALLAS, COLORES)
VALUES ('Short Cachetero', '050 051 052 053 054 055', 'Piel Cocoa Negro')
insert into GRUPOTALLACOLOR(NOMBREGTC, TALLAS, COLORES)
VALUES ('Boxer Niño', '002 004 006 008 010 012 014 016', 'Surtido')
insert into GRUPOTALLACOLOR(NOMBREGTC, TALLAS, COLORES)
VALUES ('Pantaloneta', 'S M L XL', 'Surtido')
insert into GRUPOTALLACOLOR(NOMBREGTC, TALLAS, COLORES)
VALUES ('Boxer Hombre', 'EP P M G EG', 'Surtido')
GO



/*
PROCEDIMIENTOS ALMACENADOS
*/
CREATE PROCEDURE registrarCliente
   @IDCLIENTE            varchar(15),
   @IDZONA               int,
   @TIPOID               varchar(10),
   @NOMBRE               varchar(50),
   @CONTACTO             varchar(50) = null,
   @TELEFONO             varchar(10) = null,
   @EMAIL                varchar(30) = null,
   @DIRECCION            varchar(100)         
AS 
BEGIN 
     INSERT INTO CLIENTE
     ( 
            IDCLIENTE,
            IDZONA,
            TIPOID,
            NOMBRE,
            CONTACTO,
            TELEFONO,
            EMAIL,
			DIRECCION
     ) 
     VALUES 
     ( 
            @IDCLIENTE,
            @IDZONA,
            @TIPOID,
            @NOMBRE,
            @CONTACTO,
            @TELEFONO,
            @EMAIL,
			@DIRECCION                 
     ) 
END 
GO

CREATE PROCEDURE actualizarCliente
   @IDCLIENTE            varchar(15),
   @IDZONA               int,
   @TIPOID               varchar(10),
   @NOMBRE               varchar(50),
   @CONTACTO             varchar(50) = null,
   @TELEFONO             varchar(10) = null,
   @EMAIL                varchar(30) = null,
   @DIRECCION            varchar(100)         
AS 
BEGIN 
     UPDATE CLIENTE SET
            IDZONA = @IDZONA,
            TIPOID = @TIPOID,
            NOMBRE = @NOMBRE,
            CONTACTO = @CONTACTO,
            TELEFONO = @TELEFONO,
            EMAIL = @EMAIL,
			DIRECCION = @DIRECCION
     WHERE IDCLIENTE = @IDCLIENTE
END 
GO

create PROCEDURE registrarVendedor
   @IDVENDEDOR            varchar(15),
   @NOMBRE               varchar(50),
   @ESTADO				  bit,
   @TELEFONO             varchar(10) = null,
   @DIRECCION            varchar(100) = null,
   @N1DESDE            money,
   @N2DESDE             money,
   @N3DESDE                money,
   @N4DESDE            money,
   @N5DESDE            money,
   @N1HASTA             money,
   @N2HASTA                money,
   @N3HASTA            money,
   @N4HASTA             money,
   @N5HASTA                money,
   @N1PORCENTAJE            decimal(3,2),
   @N2PORCENTAJE            decimal(3,2),
   @N3PORCENTAJE            decimal(3,2),
   @N4PORCENTAJE            decimal(3,2),
   @N5PORCENTAJE            decimal(3,2)       
AS 
BEGIN 
     INSERT INTO VENDEDOR
     ( 
            IDVENDEDOR,
			NOMBRE,
			ESTADO,
			TELEFONO,
			DIRECCION,
			   N1DESDE,
			   N2DESDE,
			   N3DESDE,
			   N4DESDE,
			   N5DESDE,
			   N1HASTA,
			   N2HASTA,
			   N3HASTA,
			   N4HASTA,
			   N5HASTA,
			   N1PORCENTAJE,
			   N2PORCENTAJE,
			   N3PORCENTAJE,
			   N4PORCENTAJE,
			   N5PORCENTAJE
     ) 
     VALUES 
     ( 
            @IDVENDEDOR,
		   @NOMBRE,
		   @ESTADO,
		   @TELEFONO,
		   @DIRECCION,
		   @N1DESDE,
		   @N2DESDE,
		   @N3DESDE,
		   @N4DESDE,
		   @N5DESDE,
		   @N1HASTA,
		   @N2HASTA,
		   @N3HASTA,
		   @N4HASTA,
		   @N5HASTA,
		   @N1PORCENTAJE,
		   @N2PORCENTAJE,
		   @N3PORCENTAJE,
		   @N4PORCENTAJE,
		   @N5PORCENTAJE    
     ) 
END 
GO

CREATE PROCEDURE actualizarVendedor
   @IDVENDEDOR            varchar(15),
   @NOMBRE               varchar(50),
   @ESTADO				  bit,
   @TELEFONO             varchar(10) = null,
   @DIRECCION            varchar(100) = null,
   @N1DESDE            money,
   @N2DESDE             money,
   @N3DESDE                money,
   @N4DESDE            money,
   @N5DESDE            money,
   @N1HASTA             money,
   @N2HASTA                money,
   @N3HASTA            money,
   @N4HASTA             money,
   @N5HASTA                money,
   @N1PORCENTAJE            decimal(3,2),
   @N2PORCENTAJE            decimal(3,2),
   @N3PORCENTAJE            decimal(3,2),
   @N4PORCENTAJE            decimal(3,2),
   @N5PORCENTAJE            decimal(3,2)        
AS 
BEGIN 
     UPDATE VENDEDOR SET
            NOMBRE = @NOMBRE,
			ESTADO = @ESTADO,
			TELEFONO = @TELEFONO,
			DIRECCION = @DIRECCION,
			   N1DESDE = @N1DESDE,
			   N1HASTA = @N1HASTA,
			   N1PORCENTAJE = @N1PORCENTAJE,
			   N2DESDE = @N2DESDE,
			   N2HASTA = @N2HASTA,
			   N2PORCENTAJE = @N2PORCENTAJE,
			   N3DESDE = @N3DESDE,
			   N3HASTA = @N3HASTA,
			   N3PORCENTAJE = @N3PORCENTAJE,
			   N4DESDE = @N4DESDE,
			   N4HASTA = @N4HASTA,
			   N4PORCENTAJE = @N4PORCENTAJE,
			   N5DESDE = @N5DESDE,
			   N5HASTA = @N5HASTA,
			   N5PORCENTAJE = @N5PORCENTAJE
     WHERE IDVENDEDOR = @IDVENDEDOR
END 
GO

CREATE PROCEDURE registrarProveedor
   @IDPROVEEDOR            varchar(15),
   @TIPOID               varchar(10),
   @NOMBRE               varchar(50),
   @CONTACTO             varchar(50) = null,
   @TELEFONO             varchar(10) = null,
   @EMAIL                varchar(30) = null,
   @DIRECCION            varchar(100),
   @ESTADO				  bit      
AS 
BEGIN 
     INSERT INTO PROVEEDOR
     ( 
            IDPROVEEDOR,
            TIPOID,
            NOMBRE,
            CONTACTO,
            TELEFONO,
            EMAIL,
			DIRECCION,
			ESTADO
     ) 
     VALUES 
     ( 
            @IDPROVEEDOR,
            @TIPOID,
            @NOMBRE,
            @CONTACTO,
            @TELEFONO,
            @EMAIL,
			@DIRECCION,
			@ESTADO               
     ) 
END 
GO

CREATE PROCEDURE actualizarProveedor
   @IDPROVEEDOR            varchar(15),
   @TIPOID               varchar(10),
   @NOMBRE               varchar(50),
   @CONTACTO             varchar(50) = null,
   @TELEFONO             varchar(10) = null,
   @EMAIL                varchar(30) = null,
   @DIRECCION            varchar(100),
   @ESTADO				  bit      
AS 
BEGIN 
     UPDATE PROVEEDOR SET
            TIPOID = @TIPOID,
            NOMBRE = @NOMBRE,
            CONTACTO = @CONTACTO,
            TELEFONO = @TELEFONO,
            EMAIL = @EMAIL,
			DIRECCION = @DIRECCION,
			ESTADO = @ESTADO
     WHERE IDPROVEEDOR = @IDPROVEEDOR
END 
GO

create PROCEDURE modifyParams
	@IVA             decimal(3,2),
	@CLIENTE         nvarchar(20)
AS
BEGIN
	UPDATE PARAMETROS SET
            IVA = @IVA,
            CLIENTE = @CLIENTE
    WHERE PARAMID = 1
END
go

create PROCEDURE modificarGTC
	@NOMBREGTCANT         varchar(20),
	@NOMBREGTC            varchar(20),
	@TALLAS               varchar(200),
	@COLORES              varchar(200)
AS
BEGIN
	UPDATE GRUPOTALLACOLOR SET
            NOMBREGTC = @NOMBREGTC,
            TALLAS = @TALLAS,
			COLORES = @COLORES
    WHERE NOMBREGTC = @NOMBREGTCANT
END
go

create PROCEDURE guardarGTC
	@NOMBREGTC            varchar(20),
	@TALLAS               varchar(200),
	@COLORES              varchar(200)
AS
BEGIN
	insert into GRUPOTALLACOLOR(NOMBREGTC, TALLAS, COLORES)
    values(@NOMBREGTC, @TALLAS, @COLORES)
END
go

create PROCEDURE addUser
	@USUARIO              varchar(15),
	@CONTRASENIA          nvarchar(50),
	@NOMBRE               varchar(50),
	@CLIENTEL             bit,
	@CLIENTEE             bit,
	@PROVEEDORL           bit,
	@PROVEEDORE           bit,
	@INVENTARIOL          bit,
	@INVENTARIOE          bit,
	@FACTURAL             bit,
	@FACTURAE             bit,
	@VENDEDORL            bit,
	@VENDEDORE            bit,
	@ADMINISTRACIONL      bit,
	@ADMINISTRACIONE      bit,
	@ESTADO				  bit
AS
BEGIN
	DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    INSERT INTO USUARIO(USUARIO, CONTRASENIA, NOMBRE, CLIENTEL, CLIENTEE, PROVEEDORL, PROVEEDORE, INVENTARIOL, INVENTARIOE, FACTURAL, FACTURAE, VENDEDORL, VENDEDORE, ADMINISTRACIONL, ADMINISTRACIONE, Salt, ESTADO)
    VALUES(@USUARIO, HASHBYTES('SHA2_512', @CONTRASENIA + CAST(@salt AS NVARCHAR(36))), @NOMBRE, @CLIENTEL, @CLIENTEE, @PROVEEDORL, @PROVEEDORE, @INVENTARIOL, @INVENTARIOE, @FACTURAL, @FACTURAE, @VENDEDORL, @VENDEDORE, @ADMINISTRACIONL, @ADMINISTRACIONE, @salt, @ESTADO)
END
go

create PROCEDURE modifyUser
	@USUARIOANT			  varchar(15),
	@USUARIO              varchar(15),
	@CONTRASENIA          nvarchar(50),
	@NOMBRE               varchar(50)
AS
BEGIN
	UPDATE USUARIO SET
            USUARIO = @USUARIO,
            CONTRASENIA = HASHBYTES('SHA2_512', @CONTRASENIA + CAST(Salt AS NVARCHAR(36))),
            NOMBRE = @NOMBRE
    WHERE USUARIO = @USUARIOANT
END
go

create PROCEDURE modifyUserAdmin
	@USUARIOANT			  varchar(15),
	@USUARIO              varchar(15),
	@CONTRASENIA          nvarchar(50),
	@NOMBRE               varchar(50),
	@CLIENTEL             bit,
	@CLIENTEE             bit,
	@PROVEEDORL           bit,
	@PROVEEDORE           bit,
	@INVENTARIOL          bit,
	@INVENTARIOE          bit,
	@FACTURAL             bit,
	@FACTURAE             bit,
	@VENDEDORL            bit,
	@VENDEDORE            bit,
	@ADMINISTRACIONL      bit,
	@ADMINISTRACIONE      bit,
	@ESTADO				  bit
AS
BEGIN
	UPDATE USUARIO SET
            USUARIO = @USUARIO,
            CONTRASENIA = HASHBYTES('SHA2_512', @CONTRASENIA + CAST(Salt AS NVARCHAR(36))),
            NOMBRE = @NOMBRE,
			CLIENTEL = @CLIENTEL,
			CLIENTEE = @CLIENTEE,
			PROVEEDORL = @PROVEEDORL,
			PROVEEDORE = @PROVEEDORE,
			INVENTARIOL = @INVENTARIOL,
			INVENTARIOE = @INVENTARIOE,
			FACTURAL = @FACTURAL,
			FACTURAE = @FACTURAE,
			VENDEDORL = @VENDEDORL,
			VENDEDORE = @VENDEDORE,
			ADMINISTRACIONL = @ADMINISTRACIONL,
			ADMINISTRACIONE = @ADMINISTRACIONE,
			ESTADO = @ESTADO
    WHERE USUARIO = @USUARIOANT
END
go

create PROCEDURE userLogin
    @USUARIO			NVARCHAR(15),
    @CONTRASENIA		NVARCHAR(50),
    @responseMessage	NVARCHAR(250)='' OUTPUT
AS
BEGIN
	DECLARE @userID INT
    IF EXISTS (SELECT USUARIO FROM USUARIO WHERE USUARIO = @USUARIO)
    BEGIN
		SET @userID = (SELECT ESTADO FROM USUARIO WHERE USUARIO = @USUARIO AND CONTRASENIA = HASHBYTES('SHA2_512', @CONTRASENIA + CAST(Salt AS NVARCHAR(36))))
		IF(@userID IS NULL)
           SET @responseMessage='Incorrect password'
		ELSE if(@userID = 1)
			set @responseMessage='Usuario inactivo'
		else
           SET @responseMessage='User successfully logged in'
    END
    ELSE
       SET @responseMessage='Invalid login'
END
go

CREATE PROCEDURE registrarArticulo
   @REFERENCIA           varchar(15),
   @IDPROVEEDOR          varchar(15),
   @NOMBREGTC            varchar(20),
   @NOMBREART            varchar(100),
   @PRECIOCOMPRA         money,
   @PRECIOVENTA          money,    
   @ESTADO				bit
AS 
BEGIN 
     INSERT INTO ARTICULO
     ( 
            REFERENCIA,
		   IDPROVEEDOR,
		   NOMBREGTC,
		   NOMBREART,
		   PRECIOCOMPRA,
		   PRECIOVENTA,
		   ESTADO
     ) 
     VALUES 
     ( 
             @REFERENCIA,
		   @IDPROVEEDOR,
		   @NOMBREGTC,
		   @NOMBREART,
		   @PRECIOCOMPRA,
		   @PRECIOVENTA,
		   @ESTADO
     ) 
END 
GO

CREATE PROCEDURE actualizarArticulo
   @REFERENCIA           varchar(15),
   @IDPROVEEDOR          varchar(15),
   @NOMBREGTC            varchar(20),
   @NOMBREART            varchar(100),
   @PRECIOCOMPRA         money,
   @PRECIOVENTA          money,
    @ESTADO				bit     
AS 
BEGIN 
     UPDATE ARTICULO SET
		   IDPROVEEDOR = @IDPROVEEDOR,
		   NOMBREGTC = @NOMBREGTC,
		   NOMBREART = @NOMBREART,
		   PRECIOCOMPRA = @PRECIOCOMPRA,
		   PRECIOVENTA = @PRECIOVENTA,
		   ESTADO = @ESTADO
     WHERE REFERENCIA = @REFERENCIA
END 
GO

CREATE PROCEDURE registrarEntrada
   @REFERENCIA           varchar(15),
   @TALLA                varchar(10),
   @COLOR                varchar(20),
   @CANTIDAD             int
AS 
BEGIN 
     INSERT INTO TYCXPRODUCTO
     ( 
            REFERENCIA,
		   TALLA,
		   COLOR,
		   CANTIDAD
     ) 
     VALUES 
     ( 
             @REFERENCIA,
		   @TALLA,
		   @COLOR,
		   @CANTIDAD
     ) 
END 
go

CREATE PROCEDURE actualizarEntrada
   @REFERENCIA           varchar(15),
   @TALLA                varchar(10),
   @COLOR                varchar(20),
   @CANTIDAD             int
AS 
BEGIN 
	UPDATE TYCXPRODUCTO SET
		   CANTIDAD = @CANTIDAD
     WHERE REFERENCIA = @REFERENCIA AND TALLA = @TALLA AND COLOR = @COLOR
END 
go

create PROCEDURE registrarFactura
   @IDVENDEDOR           varchar(15),
   @IDCLIENTE            varchar(15),
   @FECHA                datetime,
   @TIPO                 bit,
   @EMITIDO              bit,
   @NUMEROFACT           varchar(20) = null,
   @OBSERVACION			varchar(200) = null,
   @PRECIOTOTAL			money,
   @SUBTOTAL			money,
   @IVA					decimal(3,2)
AS 
BEGIN 
     INSERT INTO FACTURA
     ( 
           IDVENDEDOR,
		   IDCLIENTE,
		   FECHA,
		   TIPO,
		   EMITIDO,
		   NUMEROFACT,
		   OBSERVACION,
		   PRECIOTOTAL,
		   SUBTOTAL,
		   IVA
     ) 
     VALUES 
     ( 
           @IDVENDEDOR,
		   @IDCLIENTE,
		   @FECHA,
		   @TIPO,
		   @EMITIDO,
		   @NUMEROFACT,
		   @OBSERVACION,
		   @PRECIOTOTAL,
		   @SUBTOTAL,
		   @IVA
     ) 
END 
go

create PROCEDURE actualizarEmisionFact
   @EMITIDO              bit,
   @NUMEROFACT			varchar(20),
   @IDFACTURA            int
AS 
BEGIN 
	UPDATE FACTURA SET
		   EMITIDO = @EMITIDO,
		   NUMEROFACT = @NUMEROFACT
     WHERE IDFACTURA = @IDFACTURA
END 
go

CREATE PROCEDURE registrarDetalleFactura
   @IDFACTURA            int,
   @REFERENCIA           varchar(15),
   @CANTIDAD             int,
   @REFERENCIAF          varchar(15),
   @DESCRIPCION          varchar(100),
   @PRECIOUNIT           money,
   @PRECIOTOT            money
AS 
BEGIN 
     INSERT INTO DETALLEFACTURA
     ( 
           IDFACTURA,
		   REFERENCIA,
		   CANTIDAD,
		   REFERENCIAF,
		   DESCRIPCION,
		   PRECIOUNIT,
		   PRECIOTOT
     ) 
     VALUES 
     ( 
           @IDFACTURA,
		   @REFERENCIA,
		   @CANTIDAD,
		   @REFERENCIAF,
		   @DESCRIPCION,
		   @PRECIOUNIT,
		   @PRECIOTOT
     ) 
END 
go



/*
CREAR VISTAS
*/
create view ClienteZona as
select IDCLIENTE as 'Identificacion', NOMBRE as 'Nombre', (CIUDAD + ' - ' + PROVINCIA) as 'Zona'
from CLIENTE c join ZONA z
on c.IDZONA = z.IDZONA
go

/*create view ZonaCiudadProvincia as
select (CIUDAD + ' - ' + PROVINCIA) as 'Zona'
from ZONA
go*/

create view ClienteYZona as
select IDCLIENTE, TIPOID, NOMBRE, CONTACTO, TELEFONO, EMAIL, DIRECCION, CIUDAD, PROVINCIA
from CLIENTE c join ZONA z
on c.IDZONA = z.IDZONA
go

create view vistaUsuario as
select USUARIO as 'Usuario', NOMBRE as 'Nombre', CASE WHEN ESTADO = 0 THEN 'Activo' ELSE 'Inactivo' END as 'Estado'
from USUARIO
go

create view tallaColor as
select NOMBREGTC as 'Nombre', TALLAS as 'Tallas', COLORES as 'Colores'
from GRUPOTALLACOLOR
go

create view vistaVendedor as
select IDVENDEDOR as 'Identificacion', NOMBRE as 'Nombre', CASE WHEN ESTADO = 0 THEN 'Activo' ELSE 'Inactivo' END as 'Estado'
from VENDEDOR
go

create view vistaProveedor as
select IDPROVEEDOR as 'Identificacion', NOMBRE as 'Nombre', CASE WHEN ESTADO = 0 THEN 'Activo' ELSE 'Inactivo' END as 'Estado'
from PROVEEDOR
go

create view vistaArticuloTallas as
select TALLA as 'Talla', COLOR as 'Color', CANTIDAD as 'Cantidad', REFERENCIA
from TYCXPRODUCTO
go

create view vistaArticulo as
select REFERENCIA as 'Referencia', NOMBREART as 'Nombre', CASE WHEN ESTADO = 0 THEN 'Activo' ELSE 'Inactivo' END as 'Estado'
from ARTICULO
go

create view vistaFactura as
select IDFACTURA as 'Identificacion', v.NOMBRE as 'Vendedor', c.NOMBRE as 'Cliente', CASE WHEN EMITIDO = 0 THEN 'No' ELSE 'Si' END as 'Emitido', PRECIOTOTAL as 'Total'
from FACTURA f join VENDEDOR v
on f.IDVENDEDOR = v.IDVENDEDOR
join CLIENTE c
on f.IDCLIENTE = c.IDCLIENTE
go

create view articuloProv as
select p.NOMBRE, a.NOMBREART
from ARTICULO a join PROVEEDOR p
on a.IDPROVEEDOR = p.IDPROVEEDOR
go




/*
SELECTS
*/
select * from CLIENTE
select * from ClienteZona
select * from ClienteYZona
select * from USUARIO
select * from ZONA
select * from PARAMETROS
select * from GRUPOTALLACOLOR
select * from VENDEDOR
select * from PROVEEDOR
select * from ARTICULO
select * from TYCXPRODUCTO
select * from FACTURA
select * from DETALLEFACTURA
--select * from ZonaCiudadProvincia
go



/*
EJECUTAR PROCEDIMIENTOS ALMACENADOS
*/
exec registrarCliente "0992889128001", 1, "RUC", "DAVIDTEX S.A.", "ADRIANITA FASHION-ACHANCE LEONARDO", "042335284", null, "OLMEDO 238 Y CHILE. JUNTO AL HOTEL DORADO"
exec addUser "admin", "admin", "Patricia Calderon", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0
exec addUser "user", "user", "Usuario1", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1

DECLARE @responseMessage1 NVARCHAR(250)
exec userLogin 'user', 'user', @responseMessage = @responseMessage1 OUTPUT
PRINT @responseMessage1
GO
