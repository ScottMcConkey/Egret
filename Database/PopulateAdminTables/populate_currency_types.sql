/**************************************************
* populate_currency_types.sql
**************************************************/

insert into admin.currency_types(
	id, name, symbol, sortorder, abbreviation)
	values ('United States Dollars', '$', 1, 'USD');
	
insert into admin.currency_types(
	id, name, symbol, sortorder, abbreviation)
	values ('Nepali Rupees', '??', 2, 'NPR');
	
insert into admin.currency_types(
	id, name, symbol, sortorder, abbreviation)
	values ('Indian Rupees', '?', 3, 'INR');