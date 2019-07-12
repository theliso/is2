use si2
go

if OBJECT_ID('si2.insertSubmission') is not null
	drop proc si2.insertSubmission
if OBJECT_ID('si2.updateSubmission') is not null
	drop proc si2.updateSubmission
if OBJECT_ID('si2.deleteSubmission') is not null
	drop proc si2.deleteSubmission
go


create proc si2.insertSubmission @resumo nvarchar(50) = null, @idAuthor int = null, @idSub int = null,
@idConf int , @idSubmissao int output
as
	set transaction isolation level repeatable read

	declare @idState real;
	select @idState = e.id from dbo.Submissao as s 
		inner join dbo.Estado as e on (s.idEstado = e.id)
	where e.descricao = 'em Revisao';

	declare @id table (id int)

	if @resumo is not null
		begin

			insert into dbo.Submissao (idEstado, resumo, dataSubmissao) 
				output inserted.id into @id(id)
						values (@idState, @resumo, getdate());
	
			select @idSubmissao = id from @id	
			insert into dbo.Submissao_Conferencia (idConferencia, idSubmissao)
				values (@idConf, @idSubmissao)
		end
	else
		begin
			if @idSub is not null
				begin
					set @idSubmissao = @idSub

					if @idAuthor is not null
						insert into dbo.Autor_Submissao (idUtilizador, idSubmissao)
							values (@idAuthor, @idSubmissao)
					insert into dbo.Submissao_Conferencia (idConferencia, idSubmissao)
						values (@idConf, @idSubmissao)
				end
		end
go


create proc si2.deleteSubmission
@idSubmission int, @idConf int 
as
	set transaction isolation level repeatable read

	delete from dbo.Submissao_Conferencia 
		where idConferencia = @idConf and idSubmissao = @idSubmission 
	delete from dbo.Ficheiro where idSubmissao = @idSubmission
	delete from Revisor_Submissao where idSubmissao = @idSubmission
	delete from Autor_Submissao where idSubmissao = @idSubmission
	delete from dbo.Submissao where id = @idSubmission	

go

create proc si2.updateSubmission 
@idSubmission int = null, @estado nvarchar(10) = null,
	@resumo nvarchar(50) = null
as
	set transaction isolation level repeatable read

		if @idSubmission is null
			throw 50000, 
			'we cannot update without the submission id',
			10

		declare @idEstado real;
		select @idEstado = id from Estado where descricao = @estado;

		if @resumo is not null
			update dbo.Submissao 
				set resumo = @resumo 
			where id = @idSubmission;

		if @idEstado is not null
			update dbo.Submissao 
				set idEstado = @idEstado
			where id = @idSubmission;	
go
