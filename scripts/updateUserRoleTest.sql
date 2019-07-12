use si2;
go

if OBJECT_ID('si2.updateUserRoleTest') is not null
	drop proc si2.updateUserRoleTest
go

create proc si2.updateUserRoleTest
as
    begin transaction
    set nocount on
    exec si2.updateUserRole 3, 2
    if exists(select * from dbo.Revisor where idUtilizador = 3)
        print 'Test Passed'
    else
        print 'Test Failed'
    rollback
go

exec si2.updateUserRoleTest