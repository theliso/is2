use si2
go

if OBJECT_ID('si2.insertUser') is not null
	drop proc si2.insertUser
if OBJECT_ID('si2.deleteUser') is not null
	drop proc si2.deleteUser
if OBJECT_ID('si2.updateUser') is not null
	drop proc si2.updateUser
go


go
create proc si2.insertUser 
@mail varchar(50) = NUll, @idInstituicao int = NULL, @nome nvarchar(100) = NULL,
 @idUtilizador int output 
as
	set transaction isolation level repeatable read

	declare @id table (id int)
	insert into dbo.Utilizador(mail, idInstituicao, nome) 
	output inserted.id into @id values (@mail, @idInstituicao, @nome);
	select @idUtilizador = id from @id 
go

create proc si2.deleteUser 
@idUser int = NULL
as
	set transaction isolation level repeatable read
	if exists(select * from dbo.Revisor where idUtilizador = @idUser)
		print 'You cannot remove a user that is a reviwer'
	else if exists(select * from dbo.Conferencia where idPresidente = @idUser)
		print 'You cannot remove a user that is a president'
	else begin
		delete from dbo.Autor_Submissao where idUtilizador = @idUser
		delete from dbo.Autor where idUtilizador = @idUser
		delete from dbo.Registo where idUtilizador = @idUser
		delete from dbo.Utilizador where id = @idUser;
		end
go

create proc si2.updateUser 
@mail varchar(50) = null, @idInst int = null, @nome nvarchar(100) = null
as
	set transaction isolation level repeatable read

	if @mail is null
		throw 50000, 'should insert an email to update the user', 10
	if @idInst is not null
		update dbo.Utilizador set idInstituicao = @idInst where mail = @mail
	if @nome is not null
		update dbo.Utilizador set nome = @nome where mail = @mail
go 


