use si2;
go
if  exists (SELECT * FROM sys.schemas WHERE name = 'si2')
	drop schema si2
go
create schema si2
go
if OBJECT_ID(N'dbo.Autor_Submissao', N'U') is not null 
    drop table dbo.Autor_Submissao
if OBJECT_ID(N'dbo.Registo', N'U') is not null 
    drop table dbo.Registo
if OBJECT_ID(N'dbo.Revisor_Submissao', N'U') is not null 
    drop table dbo.Revisor_Submissao
if OBJECT_ID(N'dbo.Submissao_Conferencia', N'U') is not null 
    drop table dbo.Submissao_Conferencia
if OBJECT_ID(N'dbo.Ficheiro', N'U') is not null 
    drop table dbo.Ficheiro
if OBJECT_ID(N'dbo.Autor', N'U') is not null 
    drop table dbo.Autor
if OBJECT_ID(N'dbo.Revisor', N'U') is not null 
    drop table dbo.Revisor
if OBJECT_ID(N'dbo.Submissao', N'U') is not null 
    drop table dbo.Submissao
if OBJECT_ID(N'dbo.Conferencia', N'U') is not null 
    drop table dbo.Conferencia
if OBJECT_ID(N'dbo.Utilizador', N'U') is not null 
    drop table dbo.Utilizador
if OBJECT_ID(N'dbo.Instituicao', N'U') is not null 
    drop table dbo.Instituicao
if OBJECT_ID(N'dbo.Estado', N'U') is not null 
    drop table dbo.Estado
go


create table Estado(
    id int identity(1,1) primary key,
    descricao nvarchar(10) check (descricao in ('Em Revisao', 'Aceite', 'Rejeitado'))
);

create table Instituicao(
    id int identity(1,1) primary key,
    nome nvarchar(50) not null unique,
    morada nvarchar(50) not null,
    pais nvarchar(10) not null
);

create table Utilizador(
    id int identity(1,1) primary key,
    mail varchar(50) not null unique,
    idInstituicao int not null,
    nome nvarchar(100) not null,
    foreign key (idInstituicao) references Instituicao(id)
);

create table Conferencia(
    id int identity(1,1) primary key,
    idPresidente int,
    notaMinima int default 50,
    acronimo char(10) not null,
    nome nvarchar(50) not null,
    ano int not null,
    dataRealizacao datetime not null,
    dataLimite datetime, 
    unique(nome, ano),
    foreign key (idPresidente) references Utilizador(id)
);

create table Submissao(
    id int identity(1,1) primary key,
    idEstado int not null,
    resumo nvarchar(50),
    dataSubmissao datetime not null,
    foreign key (idEstado) references Estado(id)
);

create table Revisor(
    idUtilizador int primary key,
    foreign key (idUtilizador) references Utilizador(id)
);

create table Autor(
    idUtilizador int primary key,
    foreign key (idUtilizador) references Utilizador(id)
);

create table Ficheiro(
    id int identity(1,1) primary key,
    nome varchar(20) not null,
    extensao char(5) not null,
    idSubmissao int not null,
    unique(nome, extensao),
    foreign key (idSubmissao) references Submissao(id)
);

create table Submissao_Conferencia(
    idConferencia int,
    idSubmissao int,
    primary key(idConferencia, idSubmissao),
    foreign key (idConferencia) references Conferencia(id),
    foreign key (idSubmissao) references Submissao(id)
);

create table Revisor_Submissao(
    idSubmissao int, 
    idRevisor int,
    nota decimal(5,2) check (nota between 0 and 100),
    texto nvarchar(255),
    revisto bit not null,
    primary key (idSubmissao, idRevisor),
    foreign key (idSubmissao) references Submissao(id),
    foreign key (idRevisor) references Revisor(idUtilizador)
);

create table Registo(
    idUtilizador int, 
    idConferencia int,
    dataRegisto datetime not null,
    primary key(idUtilizador, idConferencia),
    foreign key (idConferencia) references Conferencia(id),
    foreign key (idUtilizador) references Utilizador(id)
);

create table Autor_Submissao(
    idUtilizador int, 
    idSubmissao int,
    receiveMail bit,
    primary key (idUtilizador, idSubmissao),
    foreign key (idUtilizador) references Autor(idUtilizador),
    foreign key (idSubmissao) references Submissao(id)
);