use si2
go

if OBJECT_ID('si2.insertSubmissionTest') is not null
	drop proc si2.insertSubmissionTest
if OBJECT_ID('si2.updateSubmissionTest') is not null
	drop proc si2.updateSubmissionTest
if OBJECT_ID('si2.deleteSubmissionTest') is not null
	drop proc si2.deleteSubmissionTest
go

create proc si2.insertSubmissionTest
as
    begin transaction
    set nocount on
    declare @idSubmissao int
    exec si2.insertSubmission 'resumoTest', 3, null, 2, @idSubmissao output
    if exists(select * from dbo.Submissao where id = @idSubmissao)
        print 'Test Passed'
    else 
        print 'Test Failed'
    rollback
go

create proc si2.updateSubmissionTest
as
    begin transaction
    set nocount on
    exec si2.updateSubmission 2, 'Rejeitado', 'ResumoTest'
    if exists(select * from dbo.Submissao where resumo = 'ResumoTest')
        print 'Test Passed'
    else
        print 'Test Failed'
    rollback
go

create proc si2.deleteSubmissionTest
as
    begin transaction
    set nocount on
    exec si2.deleteSubmission 1, 2
    if exists(select * from dbo.Submissao where id = 1)
        print 'Test Failed'
    else
        print 'Test Passed'
    rollback
go

exec si2.insertSubmissionTest
exec si2.updateSubmissionTest
exec si2.deleteSubmissionTest