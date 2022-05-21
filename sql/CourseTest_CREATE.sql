use hcmapplicationdb
create table CourseTests (CourseTestId int NOT NULL IDENTITY(1,1) PRIMARY KEY, CourseName nvarchar(255) NOT NULL, Author nvarchar(255), AuthorId nvarchar(255),  Questiont1 int, Questiont2 int, Questiont3 int, Questiont4 int, Questiont5 int, Questiont6 int, Questiont7 int, Questiont8 int, Questiont9 int, Questiont10  nvarchar(255));
--exec sp_rename 'Course', 'Courses';
--alter table Courses alter column CourseId int NOT NULL identity(1,1)
--alter table Courses add identity(1,1) (CourseId)

select * from CourseTests;