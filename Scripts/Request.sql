-- GetUserByUsername
select users.`Id`, users.`Username`, users.`Password`, departments.`Department`
from users
join departments
on users.`Department` = departments.`Id`
where `Username` = 'Test1';

-- AddUser
insert into users (`Username`, `Password`, `Department`)
values ("Test3", 12345, (
select `Id` 
from departments 
where `Department` = "Marketing"));