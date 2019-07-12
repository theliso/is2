use si2
go

if (OBJECT_ID('si2.updateConference ') is not null)
	drop proc si2.updateConference
go

create proc si2.updateConference 
@idConferencia int = null, @nome nvarchar(50) = NULL, @ano int = NULL,
@PresidenteMail varchar(50) = null, @dataLimite date = null, @notaMinima int = null
as
    set transaction isolation level repeatable read
        
		declare @idPres int		

		if @idConferencia is not null
			begin 
				if @notaMinima is not null
				begin
					update dbo.Conferencia 
						set notaMinima = @notaMinima
					where id = @idConferencia
					exec si2.updateSubmissionsState null, @idConferencia 
				end
				if @PresidenteMail is not null and exists(select * from dbo.Utilizador where mail = @PresidenteMail)
				begin			 
					select @idPres = id 
					from dbo.Utilizador as u 
					where u.mail = @PresidenteMail

					update dbo.Conferencia 
						set idPresidente = @idPres
					where id = @idConferencia
				end

				if @dataLimite is not null 
				begin
					update dbo.Conferencia
						set dataLimite = @dataLimite
					where id = @idConferencia
				end
			end
		else
		begin
			if @notaMinima is not NULL
			begin
				update dbo.Conferencia 
					set notaMinima = @notaMinima
				where ano = @ano and nome = @nome
				declare @id int 
				set @id = (
						  select id
						  from dbo.Conferencia 
						  where ano = @ano and nome = @nome
						  )
				exec si2.updateSubmissionsState null, @id
														
													  
			end

			if @PresidenteMail is not null and exists(select * from dbo.Utilizador where mail = @PresidenteMail)
			begin
				select @idPres = id 
				from dbo.Utilizador as u 
				where u.mail = @PresidenteMail

				update dbo.Conferencia 
					set idPresidente = @idPres
				where nome = @nome and ano = @ano
			end
			if @dataLimite is not null
			begin
				update dbo.Conferencia
					set dataLimite = @dataLimite
				where nome = @nome and ano = @ano
			end
		end
go