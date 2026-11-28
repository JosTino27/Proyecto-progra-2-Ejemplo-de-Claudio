Create database AgricolaLos3Patitos

Use AgricolaLos3Patitos

Create Table Roles (
id 		Integer Not Null Primary Key,
nombre	Varchar(50) Not Null,
estado 	Integer Not Null
)

Create Table Usuarios (
id 				Integer Not Null Primary Key,
identificacion	Varchar(20) Unique Not Null,
nombreCompleto	Varchar(100) Not Null,
nomUsuario		Varchar(30) Unique Not Null,
clave			Varchar(50) Not Null,
idRol 			Integer	Not Null,	--fk
estado			Integer Not Null,

Constraint Fk_UsrRol Foreign Key (idRol) References Roles(id)
)

Create Table Cultivos (
id					Integer Not Null Primary Key,
nombre				Varchar(50) Unique Not Null,
nombreCientifico	Varchar(100) Not Null,
estado				Integer Not Null
)

Create Table Lotes (
id				Integer Not Null Primary Key,
nombre			Varchar(50) Unique Not Null,
idCultivo		Integer Not Null,	--fk
hectareaje		decimal(18,2) Not Null,
fechaSiembra	date Not Null,
estado			Integer Not Null,

Constraint Fk_LotCul Foreign Key (idCultivo) References Cultivos(id)
)

--Herbicida, Fungicida, Insecticida, Bactericida, Fertilizante Foliar
Create Table TiposAgroquimico (
id		Integer Not Null Primary Key,
nombre	Varchar(50) Not Null,
estado	Integer Not Null
)

--Litros, Kilogramos
Create Table UnidadesMedida (
id		Integer Not Null Primary Key,
nombre	Varchar(50) Not Null,
estado	Integer Not Null
)

Create Table Agroquimicos (
id				Integer Not Null Primary Key,
nombreComercial	Varchar(50) Unique Not Null,
reactivo		Varchar(100) Not Null,
idTipo			Integer Not Null,	--fk
dosisHectarea	decimal(18,2) Not Null,
idUnidad		Integer Not Null,	--fk
estado			Integer Not Null,

Constraint Fk_AgrTip Foreign Key (idTipo) References TiposAgroquimico(id),
Constraint Fk_AgrUni Foreign Key (idUnidad) References UnidadesMedida(id)
)

--Encabezado de la aplicacion
Create Table Aplicaciones (
id		Integer Not Null Primary Key,
nombre	Varchar(50) Unique Not Null,
motivo	Varchar(200) Not Null,
estado	Integer Not Null
)

--Detalle: agroquimicos que componen la aplicacion
Create Table AplicacionDetalle (
idAplicacion	Integer Not Null,	--fk
idAgroquimico	Integer Not Null,	--fk
dosisHectarea	decimal(18,2) Not Null,
Primary key (idAplicacion, idAgroquimico),
Constraint Fk_DetApl Foreign Key (idAplicacion) References Aplicaciones(id),
Constraint Fk_DetAgr Foreign Key (idAgroquimico) References Agroquimicos(id)
)

--Encabezado del paquete (aplicaciones por cultivo)
Create Table Paquetes (
id			Integer Not Null Primary Key,
nombre		Varchar(60) Unique Not Null,
idCultivo	Integer Not Null,	--fk
estado		Integer Not Null,

Constraint Fk_PaqCul Foreign Key (idCultivo) References Cultivos(id)
)

--Detalle: aplicaciones del paquete y dias despues de siembra
Create Table PaqueteDetalle (
idPaquete			Integer Not Null,	--fk
idAplicacion		Integer Not Null,	--fk
diasDespuesSiembra	Integer Not Null,
Primary key (idPaquete, idAplicacion, diasDespuesSiembra),
Constraint Fk_PdePaq Foreign Key (idPaquete) References Paquetes(id),
Constraint Fk_PdeApl Foreign Key (idAplicacion) References Aplicaciones(id)
)

--Lo llena el programa en la ventana de Calendarizacion
Create Table Calendario (
id					Integer Not Null Primary Key,
idLote				Integer Not Null,	--fk
idPaquete			Integer Not Null,	--fk
idAplicacion		Integer Not Null,	--fk
diasDespuesSiembra	Integer Not Null,
fecha				date Not Null,

Constraint Fk_CalLot Foreign Key (idLote) References Lotes(id),
Constraint Fk_CalPaq Foreign Key (idPaquete) References Paquetes(id),
Constraint Fk_CalApl Foreign Key (idAplicacion) References Aplicaciones(id)
)


--Datos iniciales

insert into Roles values (1, 'Administrador', 1)
insert into Roles values (2, 'Digitador', 1)

insert into TiposAgroquimico values (1, 'Herbicida', 1)
insert into TiposAgroquimico values (2, 'Fungicida', 1)
insert into TiposAgroquimico values (3, 'Insecticida', 1)
insert into TiposAgroquimico values (4, 'Bactericida', 1)
insert into TiposAgroquimico values (5, 'Fertilizante Foliar', 1)

insert into UnidadesMedida values (1, 'Litros', 1)
insert into UnidadesMedida values (2, 'Kilogramos', 1)

--Primer usuario Administrador: sin el no se puede entrar al sistema
--para registrar lo demas. El resto de datos se registra desde las ventanas.
insert into Usuarios values (1, '111111', 'admin', 'Admin', 'admin123', 1, 1)
