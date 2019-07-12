use si2
GO

if OBJECT_ID('si2.calculateAcceptedSubmissionsTest') is not NULL
    drop proc si2.calculateAcceptedSubmissionsTest
go

create proc si2.calculateAcceptedSubmissionsTest
as
    declare @expected decimal(5,2)
    select @expected = si2.calculateAcceptedSubmissions()
    if (@expected = 33.33) 
        print 'Test Passed'
    else
        print 'Test Failed'
go

exec si2.calculateAcceptedSubmissionsTest