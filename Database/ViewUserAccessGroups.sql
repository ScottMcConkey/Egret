select u.username, r.name from user_roles ur
join users u
on u.id = ur.userid
join roles r
on r.id = ur.roleid
order by u.username, r.name;