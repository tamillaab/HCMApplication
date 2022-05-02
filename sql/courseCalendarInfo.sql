use hcmapplicationdb
create table CourseCalendars (CourseCalendarId int NOT NULL IDENTITY(1,1) PRIMARY KEY, Name nvarchar(255) NOT NULL, Period int, Mentor nvarchar(255), Participants int, DateOfStart Date);
--exec sp_rename 'Course', 'Courses';
--alter table Courses alter column CourseId int NOT NULL identity(1,1)
--alter table Courses add identity(1,1) (CourseId)

select * from CourseCalendars;