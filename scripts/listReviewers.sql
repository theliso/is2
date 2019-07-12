use si2
go

if OBJECT_ID('si2.listReviwers') is not null
	drop function si2.listReviwers
go

create function si2.listReviwers(@idSubmissao int, @idConf int)
returns table as

    return (
		select u.id from dbo.Utilizador as u 
		inner join dbo.Registo as reg on (u.id = reg.idUtilizador)
		inner join dbo.Conferencia as conf on (reg.idConferencia = conf.id)
	where conf.id = @idConf and u.id not in (
						select idUtilizador 
						from Autor_Submissao as au
							inner join Submissao_Conferencia as sc on (au.idSubmissao = sc.idSubmissao) 
						where sc.idSubmissao = @idSubmissao
					)
	)
go
