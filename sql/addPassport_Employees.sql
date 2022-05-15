--alter table Employees alter column Passport nvarchar(255)
update Employees set Passport='111333555' where EmployeeId = 20
select * from Employees;