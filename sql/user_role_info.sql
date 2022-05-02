use hcmapplicationdb
--create table Users (Id int NOT NULL IDENTITY(1,1) PRIMARY KEY, Email nvarchar(255) NOT NULL, Password nvarchar(255) NOT NULL, RoleId int NOT NULL);
create table Roles (Id int NOT NULL IDENTITY(1,1) PRIMARY KEY, Name nvarchar(255) NOT NULL);
--exec sp_rename 'Course', 'Courses';
--alter table Courses alter column CourseId int NOT NULL identity(1,1)
--alter table Courses add identity(1,1) (CourseId)

select * from Roles;