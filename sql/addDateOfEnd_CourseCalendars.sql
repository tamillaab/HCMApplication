--alter table CourseCalendars add DateOfEnd Date
update CourseCalendars set DateOfEnd='2022-09-10' where CourseCalendarId = 6
select * from CourseCalendars;