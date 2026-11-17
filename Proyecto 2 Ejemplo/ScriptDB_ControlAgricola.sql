-- =====================================================================
--  II Proyecto Programado - CONTROL AGRICOLA (Agricola Los 3 Patitos S.A.)
--  Script de creacion de la base de datos + datos de ejemplo
--  Ejecutar completo en SQL Server Management Studio (F5)
-- =====================================================================

Create database ControlAgricola
go

use ControlAgricola
go

-- ---------------------------------------------------------------------
--  TABLAS BASE (las 7 que pide la rubrica) + 2 tablas de detalle
-- ---------------------------------------------------------------------

-- 1. Usuarios del login
Create Table Usuarios (
id			Integer Not Null Primary Key,
nombre		Varchar(100) Not Null,			-- nombre completo
nomUsr		Varchar(60) Unique Not Null,	-- nombre de usuario para el login
clave		Varchar(60) Not Null,
tipo		Integer Not Null,				-- 0: Digitador  1: Administrador
estado		Integer Not Null				-- 0: Inactivo   1: Activo
)

-- 2. Cultivos
Create Table Cultivos (
id				Integer Not Null Primary Key,
nombre			Varchar(60) Not Null,
nomCientifico	Varchar(80) Not Null,
estado			Integer Not Null			-- 0: Inactivo   1: Activo
)

-- 3. Lotes (cada lote tiene sembrado un cultivo)
Create Table Lotes (
id				Integer Not Null Primary Key,
nombre			Varchar(60) Not Null,
idCultivo		Integer Not Null,			-- fk
hectareas		Decimal(18,2) Not Null,
fechaSiembra	Date Not Null,
estado			Integer Not Null,			-- 0: Inactivo   1: Activo
Constraint FK_LoCu Foreign Key (idCultivo) References Cultivos(id)
)

-- 4. Agroquimicos (productos aplicables)
Create Table Agroquimicos (
id			Integer Not Null Primary Key,
nombre		Varchar(60) Not Null,			-- nombre comercial
reactivo	Varchar(80) Not Null,
tipo		Integer Not Null,				-- 0: Herbicida 1: Fungicida 2: Insecticida 3: Bactericida 4: Fertilizante Foliar
dosisHa		Decimal(18,2) Not Null,			-- dosis recomendada por hectarea
unidad		Integer Not Null,				-- 0: Litros  1: Kilogramos
estado		Integer Not Null				-- 0: Inactivo   1: Activo
)

-- 5. Aplicaciones (encabezado)
Create Table Aplicaciones (
id			Integer Not Null Primary Key,
nombre		Varchar(60) Not Null,
motivo		Varchar(120) Not Null,
estado		Integer Not Null				-- 0: Inactivo   1: Activo
)

--    Detalle: agroquimicos que componen cada aplicacion
--    (On Delete Cascade: si se borra la aplicacion, se borra su detalle)
Create Table DetalleAplicaciones (
idAplicacion	Integer Not Null,			-- fk
idAgroquimico	Integer Not Null,			-- fk
Primary Key (idAplicacion, idAgroquimico),
Constraint FK_DaAp Foreign Key (idAplicacion) References Aplicaciones(id) On Delete Cascade,
Constraint FK_DaAg Foreign Key (idAgroquimico) References Agroquimicos(id)
)

-- 6. Paquetes de aplicaciones (encabezado): parametrizacion por cultivo
Create Table Paquetes (
id			Integer Not Null Primary Key,
nombre		Varchar(60) Not Null,
idCultivo	Integer Not Null,				-- fk
estado		Integer Not Null,				-- 0: Inactivo   1: Activo
Constraint FK_PaCu Foreign Key (idCultivo) References Cultivos(id)
)

--    Detalle: que aplicacion se hace y cuantos dias despues de la siembra
Create Table DetallePaquetes (
idPaquete		Integer Not Null,			-- fk
idAplicacion	Integer Not Null,			-- fk
diasSiembra		Integer Not Null,			-- dias despues de siembra para aplicar
Primary Key (idPaquete, idAplicacion, diasSiembra),
Constraint FK_DpPa Foreign Key (idPaquete) References Paquetes(id) On Delete Cascade,
Constraint FK_DpAp Foreign Key (idAplicacion) References Aplicaciones(id)
)

-- 7. Calendario: una fila por cada aplicacion que se debe hacer un dia en un lote
Create Table Calendario (
id				Integer Identity(1,1) Not Null Primary Key,
idLote			Integer Not Null,			-- fk
idPaquete		Integer Not Null,			-- fk
idAplicacion	Integer Not Null,			-- fk
fecha			Date Not Null,
Constraint FK_CaLo Foreign Key (idLote) References Lotes(id),
Constraint FK_CaPa Foreign Key (idPaquete) References Paquetes(id),
Constraint FK_CaAp Foreign Key (idAplicacion) References Aplicaciones(id)
)
go

-- ---------------------------------------------------------------------
--  DATOS DE EJEMPLO (tomados del Excel "Proyeccion de Aplicaciones")
-- ---------------------------------------------------------------------

-- Usuarios (id, nombre, nomUsr, clave, tipo, estado)
insert into Usuarios values (1, 'Administrador del Sistema', 'admin', 'admin123', 1, 1)
insert into Usuarios values (2, 'Digitador de Prueba', 'digitador', '1234', 0, 1)
insert into Usuarios values (3, 'Usuario Inactivo', 'inactivo', '1234', 0, 0)

-- Cultivos (id, nombre, nomCientifico, estado)
insert into Cultivos values (1, 'Piña', 'Ananas comosus', 1)
insert into Cultivos values (2, 'Banano', 'Musa paradisiaca', 1)
insert into Cultivos values (3, 'Yuca', 'Manihot esculenta', 0)

-- Lotes (id, nombre, idCultivo, hectareas, fechaSiembra, estado)
insert into Lotes values (1, 'Lote 1', 1, 1.00, '20250820', 1)
insert into Lotes values (2, 'Lote 2', 1, 2.50, '20250915', 1)
insert into Lotes values (3, 'Lote 3', 2, 4.00, '20251001', 1)

-- Agroquimicos (id, nombre, reactivo, tipo, dosisHa, unidad, estado)
insert into Agroquimicos values (1, 'Hyvar', 'Bromacil', 0, 2.50, 0, 1)
insert into Agroquimicos values (2, 'Gesapax', 'Ametrina', 0, 4.00, 0, 1)
insert into Agroquimicos values (3, 'Karmex', 'Diuron', 0, 2.00, 0, 1)
insert into Agroquimicos values (4, 'Assure', 'Quizalofop', 0, 2.00, 0, 1)
insert into Agroquimicos values (5, 'Aliette', 'Fosetil-Al', 1, 4.00, 1, 1)
insert into Agroquimicos values (6, 'Benlate', 'Benomil', 1, 3.00, 1, 1)
insert into Agroquimicos values (7, 'Ridomil', 'Metalaxil', 1, 3.00, 1, 1)
insert into Agroquimicos values (8, 'Basudin', 'Diazinon', 2, 3.00, 0, 1)
insert into Agroquimicos values (9, 'Mocap', 'Etoprofos', 2, 6.00, 0, 1)
insert into Agroquimicos values (10, 'Kilol', 'Extracto de semilla de citricos', 3, 4.00, 0, 1)
insert into Agroquimicos values (11, 'Zinc foliar', 'Zinc', 4, 2.00, 0, 1)
insert into Agroquimicos values (12, 'Boro foliar', 'Boro', 4, 2.00, 0, 1)
insert into Agroquimicos values (13, 'Hierro foliar', 'Hierro', 4, 2.00, 0, 1)
insert into Agroquimicos values (14, 'Fuente de fosforo', 'Fosforo', 4, 20.00, 0, 1)
insert into Agroquimicos values (15, 'Rootex', 'Enraizador', 4, 2.00, 1, 1)

-- Aplicaciones (id, nombre, motivo, estado)
insert into Aplicaciones values (1, 'Herbicida inicial', 'Control de malezas despues de la siembra', 1)
insert into Aplicaciones values (2, 'Fungicida inicial', 'Prevencion de hongos en el establecimiento', 1)
insert into Aplicaciones values (3, 'Insecticida inicial', 'Control de plagas del suelo y enraizamiento', 1)
insert into Aplicaciones values (4, 'Bactericida', 'Control de bacterias', 1)
insert into Aplicaciones values (5, 'Herbicida mantenimiento', 'Control de malezas de mantenimiento', 1)
insert into Aplicaciones values (6, 'Fungicida mantenimiento', 'Control de hongos de mantenimiento', 1)
insert into Aplicaciones values (7, 'Insecticida mantenimiento', 'Control de plagas de mantenimiento', 1)
insert into Aplicaciones values (8, 'Insecticida refuerzo', 'Refuerzo contra insectos', 1)
insert into Aplicaciones values (9, 'Fertilizacion foliar', 'Nutricion foliar de la planta', 1)

-- DetalleAplicaciones (idAplicacion, idAgroquimico)
insert into DetalleAplicaciones values (1, 1)
insert into DetalleAplicaciones values (1, 2)
insert into DetalleAplicaciones values (1, 3)
insert into DetalleAplicaciones values (1, 4)
insert into DetalleAplicaciones values (2, 5)
insert into DetalleAplicaciones values (2, 6)
insert into DetalleAplicaciones values (2, 7)
insert into DetalleAplicaciones values (2, 8)
insert into DetalleAplicaciones values (2, 14)
insert into DetalleAplicaciones values (3, 9)
insert into DetalleAplicaciones values (3, 14)
insert into DetalleAplicaciones values (3, 15)
insert into DetalleAplicaciones values (4, 10)
insert into DetalleAplicaciones values (5, 2)
insert into DetalleAplicaciones values (5, 3)
insert into DetalleAplicaciones values (5, 4)
insert into DetalleAplicaciones values (6, 5)
insert into DetalleAplicaciones values (6, 7)
insert into DetalleAplicaciones values (7, 9)
insert into DetalleAplicaciones values (8, 8)
insert into DetalleAplicaciones values (9, 11)
insert into DetalleAplicaciones values (9, 12)
insert into DetalleAplicaciones values (9, 13)

-- Paquetes (id, nombre, idCultivo, estado)
insert into Paquetes values (1, 'Fitosanitarios Piña', 1, 1)
insert into Paquetes values (2, 'Fertilizacion foliar Piña', 1, 1)

-- DetallePaquetes (idPaquete, idAplicacion, diasSiembra)
insert into DetallePaquetes values (1, 1, 5)
insert into DetallePaquetes values (1, 2, 10)
insert into DetallePaquetes values (1, 3, 15)
insert into DetallePaquetes values (1, 4, 40)
insert into DetallePaquetes values (1, 5, 65)
insert into DetallePaquetes values (1, 6, 70)
insert into DetallePaquetes values (1, 7, 75)
insert into DetallePaquetes values (1, 4, 100)
insert into DetallePaquetes values (1, 5, 125)
insert into DetallePaquetes values (1, 6, 130)
insert into DetallePaquetes values (1, 8, 135)
insert into DetallePaquetes values (1, 4, 170)
insert into DetallePaquetes values (2, 9, 15)
insert into DetallePaquetes values (2, 9, 30)
insert into DetallePaquetes values (2, 9, 45)
insert into DetallePaquetes values (2, 9, 60)
insert into DetallePaquetes values (2, 9, 75)
insert into DetallePaquetes values (2, 9, 90)
insert into DetallePaquetes values (2, 9, 105)
insert into DetallePaquetes values (2, 9, 120)
insert into DetallePaquetes values (2, 9, 135)
insert into DetallePaquetes values (2, 9, 150)
insert into DetallePaquetes values (2, 9, 165)
insert into DetallePaquetes values (2, 9, 180)
insert into DetallePaquetes values (2, 9, 195)
insert into DetallePaquetes values (2, 9, 210)
go

-- El Calendario queda vacio: se llena desde la ventana "Calendarizacion" del sistema.
