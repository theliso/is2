@echo Creating infrastructure
@echo off

sqlcmd -S localhost -E -i %cd%\createDB.sql
sqlcmd -S localhost -E -i %cd%\createTable.sql
sqlcmd -S localhost -E -i %cd%\initialData.sql

@echo Creating store procedures, functions and triggers
@echo off

sqlcmd -S localhost -E -i %cd%\insertUser.sql

sqlcmd -S localhost -E -i %cd%\insertSubmission.sql

sqlcmd -S localhost -E -i %cd%\updateSubmissionsState.sql

sqlcmd -S localhost -E -i %cd%\updateConference.sql

sqlcmd -S localhost -E -i %cd%\updateUserRole.sql

sqlcmd -S localhost -E -i %cd%\listReviewers.sql

sqlcmd -S localhost -E -i %cd%\assignReviewerToReview.sql

sqlcmd -S localhost -E -i %cd%\registerReview.sql

sqlcmd -S localhost -E -i %cd%\calculateAcceptedSubmissions.sql

@echo End creation
@echo off
@echo Starting tests

@echo InsertUserTest
sqlcmd -S localhost -E -i %cd%\insertUserTest.sql
@echo InsertSubmissionTest
sqlcmd -S localhost -E -i %cd%\insertSubmissionTest.sql
@echo UpdateSubmissionsStateTest
sqlcmd -S localhost -E -i %cd%\updateSubmissionsStateTest.sql
@echo UpdateConferenceTest
sqlcmd -S localhost -E -i %cd%\updateConferenceTest.sql
@echo UpdateUserRoleTest
sqlcmd -S localhost -E -i %cd%\updateUserRoleTest.sql
@echo ListReviewersTest
sqlcmd -S localhost -E -i %cd%\listReviewersTest.sql
@echo AssignReviewerToReviewTest
sqlcmd -S localhost -E -i %cd%\assignReviewerToReviewTest.sql
@echo RegisterReviewTest
sqlcmd -S localhost -E -i %cd%\registerReviewTest.sql
@echo CalculateAccepted
sqlcmd -S localhost -E -i %cd%\calculateAcceptedSubmissionsTest.sql
@echo off

@echo EndTest
@echo off


