use si2
go

if OBJECT_ID('si2.listReviwersTest') is not null
	drop proc si2.listReviwersTest
go

create proc si2.listReviwersTest
as
set nocount on
    if exists(select * from  si2.listReviwers(2, 2) where id = 3)
        if exists (select * from  si2.listReviwers(2, 2) where id = 2)
            print 'Test Failed'
        else 
            print 'Test Passed'
go

exec si2.listReviwersTest