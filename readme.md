# Egret

Egret is a light-weight inventory system built using .Net Core MVC. It is designed to allow small manufacturers to track Inventory Items and Consumption Events to allow for accurate stock take and value assessment.

The administrative features allow for the setup of new Users and security Access Groups that are given explicit permissions on the system. This allows for people in different organizational roles to be assigned custom permissions based upon their role.

The administrative features also allow for the configuration of Category Types for the Inventory Items as well as configuration of Units that correspond to the quantity of material purchased. The ability to create different Currencies also exists, but no conversion system is present at this point in time.

Features for Excel reporting are currently under development.

Egret currently uses a PostgreSQL database but has been designed to allow for the use of alternative databases. Stored procedures have been avoided in favor of Entity Framework Core ORM access.

The installation instructions assume the use of IIS on Microsoft Windows.