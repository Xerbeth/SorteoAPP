-------- script de sorteos -----

/*****************************************************
Script:     Sorteos
Abstract:   Base de datos para para el app de Sorteos.
Date:       27 de Octubre de  2019            
*****************************************************/
-- Database: Sorteos

-- DROP DATABASE "Sorteos";

CREATE DATABASE "Sorteos"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Colombia.1252'
    LC_CTYPE = 'Spanish_Colombia.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

/*****************************************************
Script:     InventarioPremios
Abstract:   Entidad para almacenar la información 
            respectiva de los permios del sorteo.
Date:       27 de Octubre de  2019            
*****************************************************/
CREATE TABLE InventarioPremios(
	IdInventarioPremio SERIAL PRIMARY KEY, -- PK
	Descripcion varchar(250) NOT NULL,
	Cantidad int NOT NULL,
	FechaRegistro TIMESTAMP default CURRENT_TIMESTAMP -- Fecha registro
);

/*****************************************************
Script:     Insert InventarioPremios 
Abstract:   Permite precargar al inventario de premios 
            los posibles obsequios  
Date:       27 de Octubre de  2019            
*****************************************************/
INSERT INTO inventariopremios (descripcion, cantidad)
VALUES ('Balón Blanco', 3);
INSERT INTO inventariopremios (descripcion, cantidad)
VALUES ('Balón Negro', 4);
INSERT INTO inventariopremios (descripcion, cantidad)
VALUES ('Dulce Blanco', 2);

/*****************************************************
Script:     Select InventarioPremios 
Abstract:   Permite consultar todos lo registros de la 
            tabla InventarioPremios  
Date:       27 de Octubre de  2019            
*****************************************************/
SELECT IdInventarioPremio AS IdInventarioPremio,
		Descripcion AS Descripcion,
		Cantidad AS Cantidad,
		FechaRegistro AS FechaRegistro
FROM inventariopremios

/*****************************************************
Script:     Personas
Abstract:   Entidad para almacenar la informacion de
            las personas participantes del sorteo
Date:       27 de Octubre de  2019            
*****************************************************/
CREATE TABLE Personas(
	IdPersona SERIAL PRIMARY KEY, --PK
	TipoDocumento varchar(3) NOT NULL,
	NumeroDocumento varchar(15) NOT NULL,
	PrimerNombre varchar(25) NOT NULL,
	SegundoNombre varchar(25) NULL,
	PrimerApellido varchar(25) NOT NULL,
	SegundoApellido varchar(25) NULL,
	Sexo varchar(10) NOT NULL, 
	FechaNacimiento date NOT NULL, 
	FechaRegistro TIMESTAMP default CURRENT_TIMESTAMP, -- Fecha registro
	CONSTRAINT check_TipoDocumento
    CHECK (TipoDocumento IN ('CI','CC','TI','TP','RC','CE','CI','DNI','DUI')),
	CONSTRAINT AK_NumeroDocumento UNIQUE(NumeroDocumento),
	CONSTRAINT check_Sexo
    CHECK (Sexo IN ('Masculino','Femenino'))
);


/*****************************************************
Script:     Personas
Abstract:   Entidad para almacenar la informacion de
            las personas participantes del sorteo
Date:       27 de Octubre de  2019            
*****************************************************/
INSERT INTO Personas (TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento)
VALUES ('CC','205065746572','Peter','','Norvig','','Masculino','1956-12-14');

INSERT INTO Personas (TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento)
VALUES ('CC','686563','Ken','','Thompson','','Masculino','1943-02-04');

INSERT INTO Personas (TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento)
VALUES ('CC','4061666573','James','','Gosling','','Masculino','1955-05-19');

INSERT INTO Personas (TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento)
VALUES ('CC','526362657274','Robert','','Pike','','Masculino','1956-01-01');

INSERT INTO Personas (TipoDocumento, NumeroDocumento, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, Sexo, FechaNacimiento)
VALUES ('CC','61606163','Alan','','Turing','','Masculino','1912-06-23');

/*****************************************************
Script:     GanadoresPersonasPremios
Abstract:   Entidad para almacenar la informacion de
            las personas participantes del sorteo
Date:       27 de Octubre de  2019            
*****************************************************/
CREATE TABLE Ganadores(
    IdGanadoresPersonasPremios SERIAL PRIMARY KEY,
    IdPersona INT NOT NULL,
    IdInventarioPremio INT NOT NULL,
    FechaRegistro TIMESTAMP default CURRENT_TIMESTAMP, -- Fecha registro
    FOREIGN KEY (IdPersona) REFERENCES Personas (IdPersona), -- FK Personas
    FOREIGN KEY (IdInventarioPremio) REFERENCES InventarioPremios (IdInventarioPremio) -- FK Inven
);

/*****************************************************
Script:     Ganadores
Abstract:   Función para asignar premios a las personas
Date:       27 de Octubre de  2019            
*****************************************************/
CREATE OR REPLACE FUNCTION Ganadores() RETURNS VARCHAR AS
$BODY$
DECLARE 
	cant int;
	numeroaleatorio int;
	idinventario int;
	idganador int;
	
BEGIN 

	/* Consulta la cantidad de premios disponibles */
	cant := (select sum(cantidad) from inventariopremios);
	
	IF cant = 0 THEN
		RETURN 'No existen premios para repartir.' AS respuesta;
	ELSE

		/* Ciclo para la asignación de premios */	
		FOR i IN 1 .. cant LOOP
			/*Genera un numero aleatorio entre los premios con existencia*/
			numeroaleatorio := (SELECT floor(random() * ((select count(1) from inventarioPremios where cantidad > 0)) + 1)::int);

			/* Obtiene el id del premio*/
			idinventario := (SELECT idinventariopremio from inventarioPremios where (cantidad > 0) LIMIT 1 OFFSET ((numeroaleatorio)-1));

			/* Genera numero aleatorio entre las personas registtradas*/
			numeroaleatorio := (SELECT floor(random() * ((select count(1) from personas)) + 1)::int);

			/* Obtine el id de la persona ganadora*/
			idganador := (SELECT idpersona from personas LIMIT 1 OFFSET ((numeroaleatorio)-1));

			INSERT INTO ganadores (idpersona, idinventariopremio)
			VALUES (idganador, idinventario);

			UPDATE inventarioPremios SET cantidad = (cantidad-1) where idinventariopremio = idinventario;
		END LOOP;	

		RETURN 'Se han repartido todos los premios.' AS respuesta;
	END IF;
END

$BODY$
LANGUAGE plpgsql;

