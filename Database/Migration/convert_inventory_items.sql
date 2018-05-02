INSERT INTO public.inventory_items(
	code, 
	description, 
	stockvalue, 
	sellprice, 
	comment, 
	salesacct, 
	stockacct, 
	cogacct, 
	stocktakenewqty, 
	flags,  
	sohcount, 
	buyprice, 
	qtybrksellprice, 
	sellcurrency, 
	buycurrency, 
	costprice,
	isconversion,
	conversionsource
	)
	
	select 
	"Code", --code 
	coalesce("Description", 'Conversion - No Description Provided'), --description
	cast("StockValue" as double precision), --stockvalue
	cast("SellPrice" as double precision), --sellprice
	'CONVERSION' || E'\n' ||
		'Original Comment: ' || coalesce("Comment", '') || E'\n' ||
		'Original SellUnit: ' || coalesce("SellUnit", '') || E'\n' ||
		'Original BuyUnit: ' || coalesce("BuyUnit", '') || E'\n' ||
		'Original Buy Currency: ' || coalesce("BuyPriceCurrency", '') || E'\n'
		'Original Category: ' || coalesce("Category1", ''), --comment
	cast("SalesAcct" as integer), --salesacct
	cast("StockAcct" as integer), --stockacct
	cast("COGAcct" as integer), --cogacct
	cast(trim("StockTakeNewQty") as double precision), --stocktakenewqty
	cast("Flags" as integer), --flags
	cast("SOH/Count" as double precision), --sohcount
	cast("BuyPrice" as double precision), --buyprice
	cast("QtyBrkSellPriceA1" as double precision), --qtybrksellprice
	"BuyPriceCurrency", --sellcurrency
	"BuyPriceCurrency", --buycurrency
	cast("CostPrice" as double precision), --costprice
	True, --isconversion
	'Money Works Gold' --conversionsource
	from public.migrate_inventory;
	
commit;
    
