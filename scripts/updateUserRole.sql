use si2;
go

if OBJECT_ID('si2.updateUserRole') is not null
	drop proc si2.updateUserRole
go


create proc si2.updateUserRole @idUser int = NULL, @idConf int =  NULL
as
	set transaction isolation level repeatable read
    declare @idRev real
    if(@idUser is null or @idConf is null)
        throw 50000, 'id = null', 1;
    if Exists (
		select * from dbo.Registo where idUtilizador = @idUser
		)
       		 insert into dbo.Revisor(idUtilizador) values (@idUser)
	else 
		if Exists (select * from dbo.Conferencia where id = @idConf)
		begin
			declare @date datetime
			insert into dbo.Registo (idUtilizador, idConferencia, dataRegisto) 
				values (@idUser, @idConf, GETDATE())
			insert into dbo.Revisor(idUtilizador) values (@idUser)
		end
		else
			throw 50000, 'the conference does not exist', 10
go
