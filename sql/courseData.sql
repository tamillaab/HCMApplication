use hcmapplicationdb
--insert into Courses(Name, Description, Author, Format, Trainer, Validity, Period) values (N'Лидерские качества', N'Повышение лидерских качеств у лучшего мастера', N'Леонид Васильев', N'Семинар', N'Артем Галяпин', N'3 года', 80), (N'Новые возможности Java', N'Изучение и использование новых функциональных возможностей Java', N'JETBrains', N'Лекция', N'Майкл Уайт', N'2 года', 150), (N'Переход в онлайн-режим', N'Возможности перехода полной работы в онлайн-режим, плюсы и минусы.', N'Сергей Брин', N'Семинар', N'Алексей Дюма', N'1 год', 64)
--delete from Courses where CourseId is null
--drop table Course
select * from Courses;