use si2
go

if OBJECT_ID('si2.assignReviewerToReview') is not null
	drop proc si2.assignReviewerToReview
go


create proc si2.assignReviewerToReview 
@idRevisor int = null, @idSubmissao int = null
as
	set transaction isolation level repeatable read
	begin

		if not exists (
						select idUtilizador 
						from dbo.Autor_Submissao 
						where idSubmissao = @idSubmissao and idUtilizador = @idRevisor
					  )
			begin
			if exists (
				select idUtilizador from dbo.Registo as r 
				inner join dbo.Submissao_Conferencia as sc on (r.idConferencia = sc.idConferencia)
				where r.idUtilizador = @idRevisor and sc.idSubmissao = @idSubmissao
			)
				if not exists (
							select idUtilizador from dbo.Revisor as R 
							inner join dbo.Utilizador as U on (R.idUtilizador = U.id) 
							where R.idUtilizador = @idRevisor
				)
				begin

					insert into dbo.Revisor(idUtilizador) values (@idRevisor)
					insert into dbo.Revisor_Submissao(idSubmissao, idRevisor, revisto) 
					values (@idSubmissao, @idRevisor, 0)
				end
				else 
					insert into dbo.Revisor_Submissao(idSubmissao, idRevisor, revisto) 
					values (@idSubmissao, @idRevisor, 0)
			end
		else
			throw 50000, 'The reviewer is the author of this submission.', 5
	end
go

