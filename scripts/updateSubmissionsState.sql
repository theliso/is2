use si2
go

if OBJECT_ID('si2.updateSubmissionsState') is not NULL
    drop proc si2.updateSubmissionsState
go


create proc si2.updateSubmissionsState @limiteCorte int = null, @idConf int
as
    set transaction isolation level serializable 

    declare @notaMinima int 

    if @limiteCorte is not null
    begin
        set @notaMinima = @limiteCorte
        update dbo.Conferencia
             set notaMinima = @notaMinima
        where id = @idConf
    end
    else
        set @notaMinima = (
                            select notaMinima 
                            from dbo.Conferencia 
                            where id = @idConf
                          )
    declare @state int

    set @state = (select id from dbo.Estado where descricao = 'Aceite')

    update dbo.Submissao
        set idEstado = @state
    where id in (
                    select idSubmissao
                    from dbo.Revisor_Submissao
                    where nota >= @notaMinima
                )
go