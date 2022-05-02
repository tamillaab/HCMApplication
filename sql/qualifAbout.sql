use hcmapplicationdb
create table Qualifications (QualificationId int NOT NULL IDENTITY(1,1) PRIMARY KEY, FIO nvarchar(255) NOT NULL, CourseName nvarchar(255), DateOfStart Date, Grade int);
--exec sp_rename 'Course', 'Courses';
--alter table Courses alter column CourseId int NOT NULL identity(1,1)
--alter table Courses add identity(1,1) (CourseId)

select * from Qualifications;