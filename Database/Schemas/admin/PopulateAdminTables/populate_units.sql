/**************************************************
* populate_units.sql
**************************************************/

insert into admin.units (abbreviation, name, sortorder, active)
values ('kg', 'kilograms', 1, true);

insert into admin.units (abbreviation, name, sortorder, active)
values ('m', 'meters', 2, true);

insert into admin.units (abbreviation, name, sortorder, active)
values ('ea', 'each', 3, true);

insert into admin.units (abbreviation, name, sortorder, active)
values ('g/m2', 'grams per square meter', 4, true);

insert into admin.units (abbreviation, name, sortorder, active)
values ('cm', 'centimeters', 5, true);

insert into admin.units (abbreviation, name, sortorder, active)
values ('sqf', 'square feet', 6, true);
