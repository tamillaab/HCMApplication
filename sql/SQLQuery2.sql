use hcmapplicationdb
alter table Employees alter column Name nvarchar(255);
alter table Employees alter column PhoneNumber nvarchar(255);
alter table Employees alter column Email nvarchar(255);
alter table Employees alter column Address nvarchar(255);
alter table Employees alter column Married nvarchar(255);
alter table Employees alter column Benefits nvarchar(255);

truncate table Employees;