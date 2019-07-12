use si2
GO

if OBJECT_ID('si2.calculateAcceptedSubmissions') is not NULL
    drop function si2.calculateAcceptedSubmissions
go

create function si2.calculateAcceptedSubmissions()
returns decimal(5,2) as
begin
    declare @total float
    declare @accepted float

    select @total = count(*) 
    from dbo.Submissao

    select @accepted = count(*) 
    from dbo.Submissao as s
        inner join dbo.Estado as e on (s.idEstado = e.id)
    where descricao = 'Aceite'

    declare @percentage decimal(5,2)
    set @percentage = (@accepted / @total) * 100

    return @percentage
end
