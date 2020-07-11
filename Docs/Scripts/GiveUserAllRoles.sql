insert into user_roles 
select (select id 
		from users u
		where u.username = 'Bob'),
r.id
from roles r;