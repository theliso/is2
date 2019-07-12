use si2
go

if OBJECT_ID('si2.assignReviewerToReviewTest') is not null
	drop proc si2.assignReviewerToReviewTest
go

create proc si2.assignReviewerToReviewTest
as
    begin transaction
    set nocount on
    exec si2.assignReviewerToReview 4,3
    if exists(select * from dbo.Revisor_Submissao where idSubmissao = 3 and idRevisor = 4)
        print 'Test Passed'
    else
        print 'Test Failed'
    rollback
go

exec si2.assignReviewerToReviewTest
