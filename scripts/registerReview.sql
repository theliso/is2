use si2
go

if (OBJECT_ID('si2.registerReview ') is not null)
	drop proc si2.registerReview
go

create proc si2.registerReview
@idSubmissao int = null, @nota decimal(5,2) = null, @texto nvarchar(255) = null
as
    set transaction isolation level repeatable read
    if exists (select * from dbo.Submissao where id = @idSubmissao) 
        if @nota between 0 and 100
            if @texto is not null
            begin
                update dbo.Revisor_Submissao
                set nota = @nota, texto = @texto, revisto = 1
                where idSubmissao = @idSubmissao
                if @nota >= (select notaMinima from dbo.Conferencia 
                where id = (select idConferencia from dbo.Submissao_Conferencia 
                where idSubmissao = @idSubmissao))
                begin
                    update dbo.Submissao
                    set idEstado = 3
                    where id = @idSubmissao
                end
                else 
                begin
                    update dbo.Submissao
                    set idEstado = 2
                    where id = @idSubmissao
                end
            end
            else
                throw 50000, 'You should submit a text', 10
        else 
            throw 50000, 'The note must be between 0 and 100', 10
    else 
        throw 50000, 'The submission does not exist', 10
go