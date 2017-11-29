/**************************************************
* populate_units.sql
**************************************************/

insert into admin.units (abbreviation, name, sortorder)
values ('kg', 'kilograms', 1);

insert into admin.units (abbreviation, name, sortorder)
values ('m', 'meters', 2);

insert into admin.units (abbreviation, name, sortorder)
values ('ea', 'each', 3);

insert into admin.units (abbreviation, name, sortorder)
values ('g/m2', 'grams per square meter', 4);

insert into admin.units (abbreviation, name, sortorder)
values ('cm', 'centimeters', 5);

insert into admin.units (abbreviation, name, sortorder)
values ('sqf', 'square feet', 6);
