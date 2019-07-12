use si2
go

if (OBJECT_ID('si2.registerReviewTest ') is not null)
	drop proc si2.registerReviewTest
go

create proc si2.registerReviewTest
as
    begin transaction
    set nocount on
    exec si2.registerReview 2, 80, 'reviewTest'
    if exists(select * from dbo.Revisor_Submissao where texto = 'reviewTest')
        print 'Test Passed'
    else
        print 'Test Failed'
    rollback
go

exec si2.registerReviewTest