use si2
go

if OBJECT_ID('si2.insertUserTest') is not null
	drop proc si2.insertUserTest
if OBJECT_ID('si2.deleteUserTest') is not null
	drop proc si2.deleteUserTest
if OBJECT_ID('si2.updateUserTest') is not null
	drop proc si2.updateUserTest
go


go
create proc si2.insertUserTest
as
    begin transaction
    set nocount on
    declare @idUtilizador int
    exec si2.insertUser 'mailTest', 2, 'nameTest', @idUtilizador output
    if exists(select * from dbo.Utilizador where id = @idUtilizador)
        print 'Test Passed'
    else
        print 'Test Failed'
    rollback
go

create proc si2.deleteUserTest
as
    begin transaction
    set nocount on
    exec si2.deleteUser 3
    if exists(select * from dbo.Utilizador where id = 3)
        print 'Test Failed'
    else
        print 'Test Passed'
    rollback
go

create proc si2.updateUserTest
as
    begin transaction
    set nocount on
    exec si2.updateUser 'user1@mail.com', null, 'testName'
    if exists(select * from dbo.Utilizador where nome = 'testName')
        print 'Test Passed'
    else 
        print 'Test Failed'
    rollback
go

exec si2.insertUserTest
exec si2.deleteUserTest
exec si2.updateUserTest