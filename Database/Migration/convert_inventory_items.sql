INSERT INTO public.inventory_items(
	code, 
	description, 
	stockvalue, 
	sellprice, 
	comment, 
	salesacct, 
	stockacct, 
	cogacct, 
	sellunit, 
	--stocktakenewqty, 
	flags,  
	sohcount, 
	buyprice, 
	buyunit, 
	qtybrksellprice, 
	sellcurrency, 
	buycurrency, 
	costprice
	)
	
	select 
	"Code", --code 
	coalesce("Description", 'Conversion - No Description Provided'), --description, 
	cast("StockValue" as double precision), --stockvalue
	cast("SellPrice" as double precision), --sellprice
	'CONVERSION: \n' || 
		'Original Comment: ' || "Comment" || '\n' ||
		'Original SellUnit: ' || "SellUnit" || '\n' ||
		'Original BuyUnit: ' || "BuyUnit" || '\n' ||
		'Original Buy Currency: ' || "BuyPriceCurrency" || '\n'
		'Original Category: ' || "Category1", --comment
	cast("SalesAcct" as integer), --salesacct
	cast("StockAcct" as integer), --stockacct
	cast("COGAcct" as integer), --cogacct
	"SellUnit", --sellunit
	--cast(trim("StockTakeNewQty") as double precision), --stocktakenewqty
	cast("Flags" as integer), --flags
	cast("SOH/Count" as double precision), --sohcount
	cast("BuyPrice" as double precision), --buyprice
	"BuyUnit", --buyunit
	cast("QtyBrkSellPriceA1" as double precision), --qtybrksellprice
	"BuyPriceCurrency", --sellcurrency
	"BuyPriceCurrency", --buycurrency
	cast("CostPrice" as double precision) --costprice
	from public.migrate_inventory;