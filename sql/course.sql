use hcmapplicationdb
create table Courses (CourseId int NOT NULL IDENTITY(1,1) PRIMARY KEY, Name nvarchar(255), Description nvarchar(255), Author nvarchar(255), Format nvarchar(255), Trainer nvarchar(255), Validity nvarchar(255), Period int);
--exec sp_rename 'Course', 'Courses';
use hcmapplicationdb
--alter table Courses alter column CourseId int NOT NULL identity(1,1)
--alter table Courses add identity(1,1) (CourseId)

select * from Courses;