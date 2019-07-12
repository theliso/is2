use si2
go

if (OBJECT_ID('si2.updateConferenceTest ') is not null)
	drop proc si2.updateConferenceTest
go

create proc si2.updateConferenceTest
as
    begin transaction
    set nocount on
    exec si2.updateConference 2, null, null, 'user2@mail.com', null, 100
    if exists(select * from dbo.Conferencia where notaMinima = 100)
        print 'Test Passed'
    else 
        print 'Test Failed'
    rollback
go

exec si2.updateConferenceTest