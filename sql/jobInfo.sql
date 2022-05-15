use hcmapplicationdb
--create table Job (JobId int NOT NULL IDENTITY(1,1) PRIMARY KEY, FIO nvarchar(255) NOT NULL, Position nvarchar(255), Department nvarchar(255), Experience int, HireDate Date);
--exec sp_rename 'Course', 'Courses';
--alter table Courses alter column CourseId int NOT NULL identity(1,1)
--alter table Courses add identity(1,1) (CourseId)

select * from Job;