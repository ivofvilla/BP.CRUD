create database BP_CRUD
go

use BP_CRUD
go 
create table Clientes
(
	Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
	Nome varchar(100) not null,
	Email varchar(50) not null,
	Ativo bit not null DEFAULT 1
)
go
cREATE TABLE Telefones
(
	Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
	IdCliente UNIQUEIDENTIFIER NOT NULL,
	Tipo bit,
	DDD int not null,
	Numero varchar(9) not null,
	FOREIGN KEY (IdCliente) REFERENCES Clientes(Id)
)
