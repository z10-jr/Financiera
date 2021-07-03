
USE [master]
GO
/****** Object:  Database [Financiera]    Script Date: 02/05/2019 03:28:48 p. m. ******/
CREATE DATABASE [Financiera]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Financiera', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Financiera.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'financiera_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Financiera_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Financiera] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Financiera].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Financiera] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Financiera] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Financiera] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Financiera] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Financiera] SET ARITHABORT OFF 
GO
ALTER DATABASE [Financiera] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Financiera] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Financiera] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Financiera] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Financiera] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Financiera] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Financiera] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Financiera] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Financiera] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Financiera] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Financiera] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Financiera] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Financiera] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Financiera] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Financiera] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Financiera] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Financiera] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Financiera] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Financiera] SET  MULTI_USER 
GO
ALTER DATABASE [Financiera] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Financiera] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Financiera] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Financiera] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Financiera] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Financiera]
GO
/****** Object:  Table [dbo].[modulo]    Script Date: 02/05/2019 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[modulo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_modulo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[operaciones]    Script Date: 02/05/2019 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[operaciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[idModulo] [int] NULL,
 CONSTRAINT [PK_operaciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rol]    Script Date: 02/05/2019 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rol_operacion]    Script Date: 02/05/2019 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol_operacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idRol] [int] NULL,
	[idOperacion] [int] NULL,
 CONSTRAINT [PK_perfil_operacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuario]    Script Date: 02/05/2019 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[apellido] [nvarchar](50) NULL,
	[dni] [nvarchar](50) NULL,
	[domicilio] [nvarchar](50) NULL,
	[telefono] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[usuario] [nvarchar](50) NULL,
	[password] [nvarchar](200) NULL,
	[fecha] [datetime] NULL,
	[idRol] [int] NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 02/05/2019 03:28:48 p. m. sin mantenedor******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_pais] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 21/03/2021 03:28:48 p. m. Categorias de banco: 1° nivel, 2° nivel, 3° nivel******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categorias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Entidades financieras]    Script Date: 24/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Entidades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[Habilitado] [bit] NOT NULL,
	[idCategoria] [int] NULL,
	[idProvincia] [int] NULL,
 CONSTRAINT [PK_entidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 24/03/2021 03:28:48 p. m. Estados de las solicitudes******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[Observacion] [nvarchar](50) NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_estado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Provincias]    Script Date: 24/03/2021 03:28:48 p. m. Provincias por escalabilidad futura******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Provincias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[Habilitado] [bit] NOT NULL,
	[idPais] [int] NULL,
 CONSTRAINT [PK_provincia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 24/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[apellido] [nvarchar](50) NULL,
	[dni] [nvarchar](50) NULL,
	[domicilio] [nvarchar](50) NULL,
	[telefono] [nvarchar](50) NULL,
	[celular] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[fecha] [datetime] NULL, --fecha de nacimiento
	[estadoCivil] [nvarchar](50) NULL,
	[localidad] [nvarchar](50) NULL,
	[codigoPostal] [int] NULL,
	[observaciones] [nvarchar](250) NULL, --para cargar telefonos alternativos
	[empresa] [nvarchar](50) NULL,
	[cargo] [nvarchar](50) NULL,
	[cuit] [nvarchar](50) NULL,
	[domicilioEmpresa] [nvarchar](50) NULL,
	[localidadEmpresa] [nvarchar](50) NULL,
	[codigoPostalEmpresa] [nvarchar](50) NULL,
	[ingresos] [decimal] NULL,
	[antiguedad] [int] NULL,
	[madre] [nvarchar](50) NULL,
	[padre] [nvarchar](50) NULL
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vendedores]    Script Date: 24/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendedores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[DNI] [nvarchar](50) NULL,
	[Domicilio] [nvarchar](50) NULL,
	[Telefono] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Concesionario] [nvarchar](50) NULL,
	[Habilitado] [bit] NOT NULL
 CONSTRAINT [PK_vendedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TiposRechazos]    Script Date: 27/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TiposRechazos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_tiporechazo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Solicitudes]    Script Date: 24/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Solicitudes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idConyuge] [int] NULL,
	[idUsuario] [int] NULL,
	[idVendedor] [int] NULL,
	[idEntidad] [int] NULL,
	[idRechazo] [int] NULL,
	[FechaCreacion] [datetime] NULL,
	[idEstado] [int] NULL,
	[monto] [decimal](18,2) NULL,
	[codigoEntidad] [nvarchar] (50) NULL,
	[entregaGestor] [bit] NOT NULL,
	[fechaEntregaGestor] [datetime] NULL,
	[fechaDevolucionPrenda] [datetime] NULL,
	[prendaInscripta] [bit] NOT NULL,
	[fechaInscripcionPrenda] [datetime] NULL,
	[fechaUltimoPago] [datetime] NULL,
	[fechaFirma] [datetime] NULL,
	[fechaNuevaLlamada] [datetime] NULL,
	[Cuotas] [int] NULL,
	[Vehiculo] [nvarchar](250) NULL,
	[Observaciones] [nvarchar](250) NULL
 CONSTRAINT [PK_solicitud] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TiposObservaciones]    Script Date: 27/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TiposObservaciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_tipoobservacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TiposObservaciones]    Script Date: 27/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Observaciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[observacion] [nvarchar](250) NULL,
	[idTipoObservacion] [int] NULL,
	[idUsuario] [int] NULL,
	[fecha][datetime] NULL,
	[idSolicitud] [int] NULL,
 CONSTRAINT [PK_observacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SolicitudBancos]    Script Date: 27/03/2021 03:28:48 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SolicitudBancos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idSolicitud] [int] NULL,
	[idEntidad] [int] NULL,
	[CodigoSeguimiento] [nvarchar](50) NULL,
	[Fecha][datetime] NULL,
	[Plazo] [int] NULL,
	[Monto] [decimal] NULL,
	[TNA] [decimal] NULL,
	[Observaciones] [nvarchar](250) NULL
 CONSTRAINT [PK_solicitudbanco] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO



ALTER TABLE [dbo].[operaciones]  WITH CHECK ADD  CONSTRAINT [FK_operaciones_modulo] FOREIGN KEY([idModulo])
REFERENCES [dbo].[modulo] ([id])
GO
ALTER TABLE [dbo].[operaciones] CHECK CONSTRAINT [FK_operaciones_modulo]
GO
ALTER TABLE [dbo].[rol_operacion]  WITH CHECK ADD  CONSTRAINT [FK_rol_operacion_operaciones] FOREIGN KEY([idOperacion])
REFERENCES [dbo].[operaciones] ([id])
GO
ALTER TABLE [dbo].[rol_operacion] CHECK CONSTRAINT [FK_rol_operacion_operaciones]
GO
ALTER TABLE [dbo].[rol_operacion]  WITH CHECK ADD  CONSTRAINT [FK_rol_operacion_rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[rol] ([id])
GO
ALTER TABLE [dbo].[rol_operacion] CHECK CONSTRAINT [FK_rol_operacion_rol]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[rol] ([id])
GO
ALTER TABLE [dbo].[Entidades]  WITH CHECK ADD  CONSTRAINT [FK_entidad_categoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[Categorias] ([id])
GO
ALTER TABLE [dbo].[Entidades]  WITH CHECK ADD  CONSTRAINT [FK_entidad_provincia] FOREIGN KEY([idProvincia])
REFERENCES [dbo].[Provincias] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Clientes] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_conyuge] FOREIGN KEY([idConyuge])
REFERENCES [dbo].[Clientes] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_banco] FOREIGN KEY([idEntidad])
REFERENCES [dbo].[Entidades] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_vendedor] FOREIGN KEY([idVendedor])
REFERENCES [dbo].[Vendedores] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_estado] FOREIGN KEY([idEstado])
REFERENCES [dbo].[Estados] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_tiporechazo] FOREIGN KEY([idRechazo])
REFERENCES [dbo].[TiposRechazos] ([id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD  CONSTRAINT [FK_solicitud_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[SolicitudBancos]  WITH CHECK ADD  CONSTRAINT [FK_solicitudbanco_banco] FOREIGN KEY([idEntidad])
REFERENCES [dbo].[Entidades] ([id])
GO
ALTER TABLE [dbo].[SolicitudBancos]  WITH CHECK ADD  CONSTRAINT [FK_solicitudbanco_solicitud] FOREIGN KEY([idSolicitud])
REFERENCES [dbo].[Solicitudes] ([id])
GO
ALTER TABLE [dbo].[Observaciones]  WITH CHECK ADD  CONSTRAINT [FK_observacion_tipoobservacion] FOREIGN KEY([idTipoObservacion])
REFERENCES [dbo].[TiposObservaciones] ([id])
GO
ALTER TABLE [dbo].[Observaciones]  WITH CHECK ADD  CONSTRAINT [FK_observacion_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[Observaciones]  WITH CHECK ADD  CONSTRAINT [FK_observacion_solicitud] FOREIGN KEY([idSolicitud])
REFERENCES [dbo].[Solicitudes] ([id])
GO
ALTER TABLE [dbo].[Provincias]  WITH CHECK ADD  CONSTRAINT [FK_provincia_pais] FOREIGN KEY([idPais])
REFERENCES [dbo].[Paises] ([id])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuario_rol]
GO
USE [master]
GO
ALTER DATABASE [Financiera] SET  READ_WRITE 
GO


--insert into dbo.modulo (nombre) values('CanalAutos');
--insert into dbo.Paises values('Argentina',1);
--insert into dbo.TiposObservaciones values('VERAZ',1);
--insert into dbo.TiposObservaciones values('AFIP',1);
--insert into dbo.TiposObservaciones values('ANSES',1);
--insert into dbo.TiposObservaciones values('Movimiento',1);
--insert into dbo.Estados (nombre, habilitado) values('Inicial',1);
--insert into dbo.Estados (nombre, habilitado) values('En Analisis',1);
--insert into dbo.Estados (nombre, habilitado) values('Para Llamar',1);
--insert into dbo.Estados (nombre, habilitado) values('Desechado',1);
--insert into dbo.Estados (nombre, habilitado) values('Completo para Cargar',1);
--insert into dbo.Estados (nombre, habilitado) values('Desistió el Cliente',1);
--insert into dbo.Estados (nombre, habilitado) values('A Espera de Resultado',1);
--insert into dbo.Estados (nombre, habilitado) values('Para LLamar a Firmar',1);
--insert into dbo.Estados (nombre, habilitado) values('Desaprobado',1);
--insert into dbo.Estados (nombre, habilitado) values('Firmado',1);
--insert into dbo.Estados (nombre, habilitado) values('Prenda a Gestor',1);
--insert into dbo.Estados (nombre, habilitado) values('Prenda Inscripta',1);
--insert into dbo.Provincias (nombre, habilitado, idPais) values('Tucumán', 1,1);
--insert into dbo.Categorias (nombre, habilitado) values('Primera Categoria',1);
--insert into dbo.Categorias (nombre, habilitado) values('Segunda Categoria',1);
--insert into dbo.Categorias (nombre, habilitado) values('Tercera Categoria',1);
--insert into dbo.TiposRechazos (nombre, habilitado) values('Veraz',1);
--insert into dbo.Entidades (nombre, habilitado, idCategoria) values('Banco Macro', 1,1);
--insert into dbo.Entidades (nombre, habilitado, idCategoria) values('Banco ICBC', 1,1);

--insert into dbo.operaciones values('Admin OP1',1);
--insert into dbo.operaciones values('Admin OP2',1);
--insert into dbo.operaciones values('Admin OP3',1);
--insert into dbo.rol values('Administrador');
--insert into dbo.rol values('Vendedor');
--insert into dbo.rol_operacion values(1,1);
--insert into dbo.rol_operacion values(1,2);
--insert into dbo.rol_operacion values(1,3);
--insert into dbo.usuarios (nombre,	apellido,	dni,	domicilio,	telefono,	email,	usuario, password,	idRol) values('Alvin',	'Rodriguez',	'65446321',	'Lavaisse 1358',	'0303456',	'alvin@gmail.com',	'alvin',	'123',	1);

