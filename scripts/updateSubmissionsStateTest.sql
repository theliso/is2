use si2
go

if OBJECT_ID('si2.updateSubmissionsStateTest') is not NULL
    drop proc si2.updateSubmissionsStateTest
go

create proc si2.updateSubmissionsStateTest
as
    begin transaction
    set nocount on
    exec si2.updateSubmissionsState 5, 1
    declare @expected decimal(5,2)
    select @expected = si2.calculateAcceptedSubmissions()
    if (@expected = 66.67) 
        print 'Test Passed'
    else
        print 'Test Failed'
    rollback
go

exec si2.updateSubmissionsStateTest