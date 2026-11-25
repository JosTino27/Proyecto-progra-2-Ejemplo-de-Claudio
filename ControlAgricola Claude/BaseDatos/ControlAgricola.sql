--Proyecto II - Programacion II
--Control Agricola - Agricola Los 3 Patitos S.A.
--estado: 1 = activo, 0 = inactivo

Create database AgricolaLos3Patitos
go

use AgricolaLos3Patitos
go

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

insert into Usuarios values (1, '101110111', 'Administrador del Sistema', 'admin', 'admin123', 1, 1)
insert into Usuarios values (2, '202220222', 'Usuario Digitador', 'digitador', 'dig123', 2, 1)
insert into Usuarios values (3, '303330333', 'Usuario Inactivo', 'inactivo', 'ina123', 2, 0)

insert into Cultivos values (1, 'Pina', 'Ananas comosus', 1)
insert into Cultivos values (2, 'Banano', 'Musa paradisiaca', 1)
insert into Cultivos values (3, 'Yuca', 'Manihot esculenta', 0)

insert into Lotes values (1, 'Lote 1', 1, 1.00, '2025-08-20', 1)
insert into Lotes values (2, 'Lote 2', 1, 2.50, '2026-01-15', 1)
insert into Lotes values (3, 'Lote 3', 2, 3.00, '2026-03-01', 1)

--Agroquimicos del Excel "Proyeccion de Aplicaciones"
insert into Agroquimicos values (1, 'Bromacil', 'Bromacil', 1, 2.50, 1, 1)
insert into Agroquimicos values (2, 'Ametrina', 'Ametrina', 1, 4.00, 1, 1)
insert into Agroquimicos values (3, 'Diuron', 'Diuron', 1, 2.00, 1, 1)
insert into Agroquimicos values (4, 'Quizalofop', 'Quizalofop-p-etil', 1, 2.00, 1, 1)
insert into Agroquimicos values (5, 'Fosetil-Al', 'Fosetil aluminio', 2, 4.00, 2, 1)
insert into Agroquimicos values (6, 'Benomil', 'Benomil', 2, 3.00, 2, 1)
insert into Agroquimicos values (7, 'Ridomil', 'Metalaxil + Mancozeb', 2, 3.00, 2, 1)
insert into Agroquimicos values (8, 'Diazinon', 'Diazinon', 3, 3.00, 1, 1)
insert into Agroquimicos values (9, 'Fuente de fosforo', 'Fosfito de potasio', 2, 20.00, 1, 1)
insert into Agroquimicos values (10, 'Etoprofos', 'Etoprofos', 3, 6.00, 1, 1)
insert into Agroquimicos values (11, 'Rootex', 'Bioestimulante radicular', 3, 2.00, 2, 1)
insert into Agroquimicos values (12, 'Kilol', 'Extracto de citricos', 4, 4.00, 1, 1)
insert into Agroquimicos values (13, 'Zinc', 'Sulfato de zinc', 5, 2.00, 1, 1)
insert into Agroquimicos values (14, 'Boro', 'Acido borico', 5, 2.00, 1, 1)
insert into Agroquimicos values (15, 'Hierro', 'Quelato de hierro', 5, 2.00, 1, 1)
insert into Agroquimicos values (16, 'Nitrogeno liquido', 'Urea (kg urea x 1,48)', 5, 66.60, 1, 1)
insert into Agroquimicos values (17, 'KCl', 'Cloruro de potasio', 5, 25.00, 2, 1)
insert into Agroquimicos values (18, 'Calcio', 'Nitrato de calcio', 5, 3.00, 1, 1)
insert into Agroquimicos values (19, 'Magnesio', 'Sulfato de magnesio', 5, 3.00, 1, 1)

insert into Aplicaciones values (1, 'Herbicida Inicial', 'Control de malezas pre-emergente', 1)
insert into Aplicaciones values (2, 'Herbicida Mantenimiento', 'Control de malezas post-emergente', 1)
insert into Aplicaciones values (3, 'Fungicida Inicial', 'Prevencion de hongos en la raiz', 1)
insert into Aplicaciones values (4, 'Fungicida Mantenimiento', 'Control de Phytophthora', 1)
insert into Aplicaciones values (5, 'Insecticida Inicial', 'Control de plagas del suelo', 1)
insert into Aplicaciones values (6, 'Insecticida Etoprofos', 'Control de nematodos', 1)
insert into Aplicaciones values (7, 'Insecticida Diazinon', 'Control de cochinilla', 1)
insert into Aplicaciones values (8, 'Bactericida', 'Prevencion de bacteriosis', 1)
insert into Aplicaciones values (9, 'Foliar Etapa 1', 'Nutricion foliar de 15 a 60 dias', 1)
insert into Aplicaciones values (10, 'Foliar Etapa 2', 'Nutricion foliar de 75 a 105 dias', 1)
insert into Aplicaciones values (11, 'Foliar Etapa 3', 'Nutricion foliar de 120 a 165 dias', 1)
insert into Aplicaciones values (12, 'Foliar Etapa 4', 'Nutricion foliar de 180 a 210 dias', 1)

--(aplicacion, agroquimico, dosis x hectarea)
insert into AplicacionDetalle values (1, 1, 2.50)
insert into AplicacionDetalle values (1, 2, 4.00)
insert into AplicacionDetalle values (1, 3, 2.00)
insert into AplicacionDetalle values (1, 4, 2.00)
insert into AplicacionDetalle values (2, 2, 4.00)
insert into AplicacionDetalle values (2, 3, 2.00)
insert into AplicacionDetalle values (2, 4, 2.00)
insert into AplicacionDetalle values (3, 5, 4.00)
insert into AplicacionDetalle values (3, 6, 3.00)
insert into AplicacionDetalle values (3, 7, 3.00)
insert into AplicacionDetalle values (3, 8, 3.00)
insert into AplicacionDetalle values (3, 9, 20.00)
insert into AplicacionDetalle values (4, 5, 4.00)
insert into AplicacionDetalle values (4, 7, 3.00)
insert into AplicacionDetalle values (5, 10, 6.00)
insert into AplicacionDetalle values (5, 9, 20.00)
insert into AplicacionDetalle values (5, 11, 2.00)
insert into AplicacionDetalle values (6, 10, 6.00)
insert into AplicacionDetalle values (7, 8, 3.00)
insert into AplicacionDetalle values (8, 12, 4.00)
insert into AplicacionDetalle values (9, 13, 2.00)
insert into AplicacionDetalle values (9, 14, 2.00)
insert into AplicacionDetalle values (9, 15, 2.00)
insert into AplicacionDetalle values (9, 16, 66.60)
insert into AplicacionDetalle values (9, 17, 25.00)
insert into AplicacionDetalle values (9, 18, 3.00)
insert into AplicacionDetalle values (9, 19, 3.00)
insert into AplicacionDetalle values (10, 13, 2.00)
insert into AplicacionDetalle values (10, 14, 2.00)
insert into AplicacionDetalle values (10, 15, 2.00)
insert into AplicacionDetalle values (10, 16, 88.80)
insert into AplicacionDetalle values (10, 17, 25.00)
insert into AplicacionDetalle values (10, 18, 3.00)
insert into AplicacionDetalle values (10, 19, 3.00)
insert into AplicacionDetalle values (11, 13, 2.00)
insert into AplicacionDetalle values (11, 14, 2.00)
insert into AplicacionDetalle values (11, 15, 2.00)
insert into AplicacionDetalle values (11, 16, 118.40)
insert into AplicacionDetalle values (11, 17, 30.00)
insert into AplicacionDetalle values (11, 18, 3.00)
insert into AplicacionDetalle values (11, 19, 3.00)
insert into AplicacionDetalle values (12, 13, 2.00)
insert into AplicacionDetalle values (12, 14, 2.00)
insert into AplicacionDetalle values (12, 15, 2.00)
insert into AplicacionDetalle values (12, 16, 133.20)
insert into AplicacionDetalle values (12, 17, 35.00)
insert into AplicacionDetalle values (12, 18, 4.00)
insert into AplicacionDetalle values (12, 19, 4.00)

insert into Paquetes values (1, 'Fitosanitario Pina', 1, 1)
insert into Paquetes values (2, 'Fertilizacion Foliar Pina', 1, 1)

--(paquete, aplicacion, dias despues de siembra)
insert into PaqueteDetalle values (1, 1, 5)
insert into PaqueteDetalle values (1, 3, 10)
insert into PaqueteDetalle values (1, 5, 15)
insert into PaqueteDetalle values (1, 8, 40)
insert into PaqueteDetalle values (1, 2, 65)
insert into PaqueteDetalle values (1, 4, 70)
insert into PaqueteDetalle values (1, 6, 75)
insert into PaqueteDetalle values (1, 8, 100)
insert into PaqueteDetalle values (1, 2, 125)
insert into PaqueteDetalle values (1, 4, 130)
insert into PaqueteDetalle values (1, 7, 135)
insert into PaqueteDetalle values (1, 8, 170)
insert into PaqueteDetalle values (2, 9, 15)
insert into PaqueteDetalle values (2, 9, 30)
insert into PaqueteDetalle values (2, 9, 45)
insert into PaqueteDetalle values (2, 9, 60)
insert into PaqueteDetalle values (2, 10, 75)
insert into PaqueteDetalle values (2, 10, 90)
insert into PaqueteDetalle values (2, 10, 105)
insert into PaqueteDetalle values (2, 11, 120)
insert into PaqueteDetalle values (2, 11, 135)
insert into PaqueteDetalle values (2, 11, 150)
insert into PaqueteDetalle values (2, 11, 165)
insert into PaqueteDetalle values (2, 12, 180)
insert into PaqueteDetalle values (2, 12, 195)
insert into PaqueteDetalle values (2, 12, 210)
