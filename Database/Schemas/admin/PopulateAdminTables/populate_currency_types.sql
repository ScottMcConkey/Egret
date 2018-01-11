/**************************************************
* populate_currency_types.sql
**************************************************/

insert into admin.currency_types(
	id, name, symbol, sortorder, abbreviation, active, defaultselection)
	values ('United States Dollars', '$', 1, 'USD', true, false);
	
insert into admin.currency_types(
	id, name, symbol, sortorder, abbreviation)
	values ('Nepali Rupees', '??', 2, 'NPR', true, true);
	
insert into admin.currency_types(
	id, name, symbol, sortorder, abbreviation)
	values ('Indian Rupees', '?', 3, 'INR', true, false);