--alter table CourseCalendars add CourseKey nvarchar(255)
update CourseCalendars set CourseKey='15a7fe097fe64266862375c20fc6302a' where CourseCalendarId = 1
update CourseCalendars set CourseKey='eeaec6683f534685bf4059ffcb31cfb2' where CourseCalendarId = 2
update CourseCalendars set CourseKey='455083dcd0574a2b89f5e68061a2ca75' where CourseCalendarId = 3
update CourseCalendars set CourseKey='3d4207588f504e13810b4fc592660bcc' where CourseCalendarId = 4
update CourseCalendars set CourseKey='3e42b33b95a548d58784e7a8366c0f39' where CourseCalendarId = 5
update CourseCalendars set CourseKey='e7e9751182ea4839b83949131385045d' where CourseCalendarId = 6

select * from CourseCalendars;